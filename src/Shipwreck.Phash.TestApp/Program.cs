using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Shipwreck.Phash.Bitmaps;
using Shipwreck.Phash.PresentationCore;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Text;

namespace Shipwreck.Phash.TestApp
{
    internal class Program
    {
        private class FileDigests
        {
            public FileDigests(string f)
            {
                fi = new FileInfo(f);
                using (var fs = fi.OpenRead())
                {
                    using (var image = Image.FromStream(fs, true))
                    using (var bitmap = image.ToBitmap())
                    {
                        BitmapHash = ImagePhash.ComputeDigest(bitmap.ToLuminanceImage());
                        RawBitmapHash = ImagePhash.ComputeDigest(bitmap.ToRawBitmapData().ToLuminanceImage());
                    }
                    fs.Position = 0;
                    BitmapSourceHash = ImagePhash.ComputeDigest(BitmapFrame.Create(fs).ToByteImage());

                    //// TODO: Assert all digests are same
                }
            }

            public FileInfo fi;

            public Digest BitmapSourceHash;
            public Digest BitmapHash;
            public Digest RawBitmapHash;
        }

        private struct CCR
        {
            public int i;
            public int j;
            public double m;
        }

        private static void Main(string[] args)
        {
            var prg = new Program()
            {
                _Output = OutputToConsole
            };

            var targets = new List<string>();

            foreach (var a in args)
            {
                if (a == "--html")
                {
                    prg._Output = OutputToHtml;
                    continue;
                } else if (a.StartsWith("--perf"))
                {
                    prg._PerformanceTracker = new PerformanceTracker();
                    int iterationsParse;
                    if (a.IndexOf(':') > 0 && int.TryParse(a.Split(':').Last(), out iterationsParse) && iterationsParse > 0)
                        prg._Iterations = iterationsParse;
                    continue;
                }

                targets.Add(a);
            }

            prg.ProcessFiles(targets.Any() ? targets
                            : new string[] { "compr", "blur", "rotd", "misc" }.SelectMany(d => Directory.EnumerateFiles(d)));
        }

        private void ProcessFiles(IEnumerable<string> fs)
        {
            var files = fs.Select(f => Path.GetFullPath(f).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)).ToList();

            List<FileDigests> digests = new List<FileDigests>();
            List<CCR> results = new List<CCR>();
            var totalTimerId = _PerformanceTracker?.StartTimer("Total");
            for (int iteration = 0; iteration < _Iterations; iteration++)
            {
                var digestIterationTimerId = _PerformanceTracker?.StartTimer("Digest Iteration");
                digests = files.AsParallel().Select(f => {
                    var digestTimerId = _PerformanceTracker?.StartTimer("Digest");
                    var digest = new FileDigests(f);
                    _PerformanceTracker?.EndTimer(digestTimerId.GetValueOrDefault(), "Digest");
                    return digest;
                    }).ToList();
                _PerformanceTracker?.EndTimer(digestIterationTimerId.GetValueOrDefault(), "Digest Iteration");
                results = digests.SelectMany((d1, i) => digests.Skip(i).Select((d2, j) => new CCR
                {
                    i = i,
                    j = j + i,
                    m = ImagePhash.GetCrossCorrelation(d1.BitmapSourceHash, d2.BitmapSourceHash)
                })).OrderBy(v => v.m).ToList();
            }
            _PerformanceTracker?.EndTimer(totalTimerId.GetValueOrDefault(), "Total");
            var bd = GetCommonPath(files);
            var perfReport = _PerformanceTracker?.GenerateReport();
            _Output?.Invoke(bd, digests, results, perfReport);
        }

        #region Performance tracking
        private PerformanceTracker _PerformanceTracker = null;
        private int _Iterations = 1;
        
       
        class PerformanceTracker
        {

            ConcurrentDictionary<string, ConcurrentDictionary<int, Stopwatch>> categoryToIdToTimerMap = new ConcurrentDictionary<string, ConcurrentDictionary<int, Stopwatch>>();
            static int Id = 0;
            
            /// <summary>
            /// Starts a new timer instance in a category of timers.
            /// </summary>
            /// <param name="category">Group name for the timer</param>
            /// <returns>Id of the timer</returns>
            public int StartTimer(string category = "default")
            {
                var idToTimerMap = categoryToIdToTimerMap.GetOrAdd(category, (key) => new ConcurrentDictionary<int, Stopwatch>());
                return AddNewTimer(idToTimerMap);
            }

            int AddNewTimer(ConcurrentDictionary<int, Stopwatch> idToTimerMap)
            {
                int currentId = Interlocked.Increment(ref Id);
                idToTimerMap.AddOrUpdate(currentId, (key) => Stopwatch.StartNew(), 
                    (key, oldValue) => {
                        oldValue.Restart();
                        return oldValue;
                    });
                return currentId;
            }

            /// <summary>
            /// Ends a timer of the specified id and category.
            /// </summary>
            /// <param name="id">Id of the timer</param>
            /// <param name="category">Group name of the timer</param>
            /// <returns>Id of the timer</returns>
            public int EndTimer(int id, string category = "default")
            {
                ConcurrentDictionary<int, Stopwatch> idToTimerMap;
                if (!categoryToIdToTimerMap.TryGetValue(category, out idToTimerMap))
                    throw new KeyNotFoundException($"{category} key does not exist.");
                Stopwatch timer;
                if (!idToTimerMap.TryGetValue(id, out timer))
                    throw new KeyNotFoundException($"{category}:{id} key does not exist.");
                timer.Stop();
                return id;
            }

            public PerformanceReport GenerateReport()
            {
                var categoryToSummaryMap = new ConcurrentDictionary<string, PerformanceReport.SummaryStatistics>();
                var taskList = categoryToIdToTimerMap.Select(async (keyValue) => {
                    var summaryStatistics = await GenerateStatistics(keyValue.Value);
                    categoryToSummaryMap.AddOrUpdate(keyValue.Key, (key) => summaryStatistics, (key, oldValue) => summaryStatistics);
                    });
                Task.WhenAll(taskList).GetAwaiter().GetResult();
                return new PerformanceReport(categoryToSummaryMap);
            }

            async Task<PerformanceReport.SummaryStatistics> GenerateStatistics(ConcurrentDictionary<int, Stopwatch> idToTimerMap)
            {
                await Task.Yield();

                var MinEntry = idToTimerMap.Min(kv => kv.Value.IsRunning ? TimeSpan.MaxValue : kv.Value.Elapsed);
                var MaxEntry = idToTimerMap.Max(kv => kv.Value.IsRunning ? TimeSpan.MinValue : kv.Value.Elapsed);
                var AverageTimespan = TimeSpan.FromMilliseconds((MaxEntry.TotalMilliseconds - MinEntry.TotalMilliseconds) / 2);
                var Average = TimeSpan.FromMilliseconds(idToTimerMap.Average(kv => kv.Value.IsRunning ? AverageTimespan.TotalMilliseconds : kv.Value.ElapsedMilliseconds));
                var Sum = TimeSpan.FromMilliseconds(idToTimerMap.Sum(kv => kv.Value.IsRunning ? TimeSpan.Zero.TotalMilliseconds : kv.Value.ElapsedMilliseconds));
                return new PerformanceReport.SummaryStatistics(idToTimerMap.Count, MinEntry, MaxEntry, Average, Sum);
            }


            
        }

        public class PerformanceReport
        {
            public class SummaryStatistics
            {
                public SummaryStatistics(int entryCount, TimeSpan minEntry, TimeSpan maxEntry, TimeSpan average, TimeSpan sum)
                {
                    EntryCount = entryCount;
                    MinEntry = minEntry;
                    MaxEntry = maxEntry;
                    Average = average;
                    Sum = sum;
                }
                public int EntryCount { get; }
                public TimeSpan MinEntry { get; }
                public TimeSpan MaxEntry { get; }
                public TimeSpan Average { get; }
                public TimeSpan Sum { get; }

                public override string ToString()
                {
                    return $"{{Entries:{EntryCount}, Min:{MinEntry.ToString()}, Max:{MaxEntry.ToString()}, Avg:{Average.ToString()}, Sum:{Sum.ToString()}}}";
                }
            }

            ReadOnlyDictionary<string, SummaryStatistics> categoryToSummaryMap;

            public PerformanceReport(IDictionary<string, SummaryStatistics> categoryToSummaryMap)
            {
                this.categoryToSummaryMap = new ReadOnlyDictionary<string, SummaryStatistics>(categoryToSummaryMap);
            }

            public IReadOnlyDictionary<string, SummaryStatistics> CategoryToSummaryMap => categoryToSummaryMap;

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.AppendLine("Performance Report:");
                foreach (var categorySummaryPair in CategoryToSummaryMap.OrderBy(kv => kv.Key))
                {
                    builder.AppendLine("-----");
                    builder.AppendLine($"    {categorySummaryPair.Key}: {categorySummaryPair.Value.ToString()}");
                }
                builder.AppendLine("-----END------");
                return builder.ToString();

            }
        }


        #endregion

        #region Output result

        private Action<string, List<FileDigests>, List<CCR>, PerformanceReport> _Output;

        private static void OutputToConsole(string bd, List<FileDigests> digests, List<CCR> results, PerformanceReport perfReport)
        {
            foreach (var d in digests.OrderBy(d => d.fi.FullName, StringComparer.CurrentCultureIgnoreCase))
            {
                Console.WriteLine(GetShortPath(bd, d));
                Console.WriteLine(d.BitmapSourceHash);
            }
            Console.WriteLine();

            Console.WriteLine("Hit any key to continue..");
            Console.ReadKey();

            if (digests.Count > 1)
            {
                foreach (var r in results)
                {
                    var d1 = digests[r.i];
                    var d2 = digests[r.j];
                    Console.WriteLine(GetShortPath(bd, d1));
                    Console.WriteLine(GetShortPath(bd, d2));
                    Console.WriteLine(r.m.ToString("f7"));
                }
            }

            if (perfReport != null)
                Console.WriteLine(perfReport.ToString());

            Console.WriteLine("Hit any key to exit..");
            Console.ReadKey();
        }

        private static void OutputToHtml(string bd, List<FileDigests> digests, List<CCR> results, PerformanceReport perfReport)
        {
            var xd = XDocument.Load("output.template.html");

            using (var sw = new StreamWriter("output.html", false, System.Text.Encoding.UTF8))
            using (var xw = XmlWriter.Create(sw, new XmlWriterSettings()
            {
                OmitXmlDeclaration = true,
                CheckCharacters = false
            }))
            {
                var js = new JsonSerializer();
                using (var aw = new StringWriter())
                {
                    aw.Write("output(");
                    js.Serialize(aw, digests.Select(d => new { path = GetShortPath(bd, d), url = new Uri(d.fi.FullName).ToString() }));
                    aw.Write(",");
                    js.Serialize(aw, results.Select(r => new { r.i, r.j, ccr = r.m }));
                    aw.Write(",");
                    js.Serialize(aw, new { report = perfReport?.ToString() ?? "" });
                    aw.Write(")");

                    xd.Descendants("{http://www.w3.org/1999/xhtml}body").First().SetAttributeValue("onload", aw);
                }

                xd.Save(xw);
            }

            Process.Start("output.html");
        }

        #endregion Output result

        #region IO Utils

        private static string GetCommonPath(IEnumerable<string> files)
        {
            string common = null;
            foreach (var f in files)
            {
                if (common == null)
                {
                    common = Path.GetDirectoryName(f);
                }
                else
                {
                    while (f.Length <= common.Length
                            || !f.StartsWith(common)
                            || (f[common.Length] != Path.DirectorySeparatorChar
                                && f[common.Length - 1] != Path.DirectorySeparatorChar))
                    {
                        common = Path.GetDirectoryName(common);
                        if (common == null)
                        {
                            return null;
                        }
                    }
                }
            }

            if (common?.Length > 0 && common.Last() != Path.DirectorySeparatorChar)
            {
                return common + Path.DirectorySeparatorChar;
            }

            return common;
        }

        private static string GetShortPath(string bd, FileDigests d)
            => bd?.Length > 0 ? d.fi.FullName.Substring(bd.Length) : d.fi.FullName;

        #endregion IO Utils
    }
}