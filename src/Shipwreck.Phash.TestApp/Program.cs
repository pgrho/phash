using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Shipwreck.Phash.Imaging;
using System.Drawing;

namespace Shipwreck.Phash.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirs = new string[] { "compr", "blur", "rotd", "misc" };

            var bitmapSourceHashes = new Dictionary<string, Digest>();
            var bitmapHashes = new Dictionary<string, Digest>();
            var bitmapRawHashes = new Dictionary<string, Digest>();
            using (var sw = new StreamWriter("result.html"))
            {
                sw.WriteLine("<!DOCTYPE html>");
                sw.WriteLine("<html><body>");
                var collectHashes = true;
                foreach (var d in dirs)
                {
                    var di = new DirectoryInfo(d);
                    Console.WriteLine("{0}", di.FullName);

                    foreach (var fi in di.EnumerateFiles())
                    {
                        using (var fs = fi.OpenRead())
                        {
                            Digest bitmapSourceHash;
                            Digest bitmapHash;
                            Digest bitmapRawHash;
                            using (var image = Image.FromStream(fs, true))
                            using (var bitmap = image.ToBitmap())
                            {
                                bitmapHash = ImagePhash.ComputeDigest(bitmap);
                                bitmapRawHash = ImagePhash.ComputeDigest(bitmap.ToRawBitmapData());
                            }
                            fs.Position = 0;
                            bitmapSourceHash = ImagePhash.ComputeDigest(fs);
                            Console.WriteLine(" - {0}: {1:X16}", fi.Name, bitmapSourceHash);

                            if (collectHashes)
                            {
                                bitmapSourceHashes[fi.FullName] = bitmapSourceHash;
                                bitmapHashes[fi.FullName] = bitmapHash;
                                bitmapRawHashes[fi.FullName] = bitmapRawHash;
                            }
                            else
                            {
                                var bitmapSourceCCResults = CrossCorrelateDigests(bitmapSourceHashes, bitmapSourceHash);
                                var bitmapCCResults = CrossCorrelateDigests(bitmapHashes, bitmapHash);
                                var bitmapRawCCResults = CrossCorrelateDigests(bitmapRawHashes, bitmapRawHash);

                                var mismatchedCCResults = MatchCrossCorrelationResults("bitmap", bitmapCCResults, "bitmap_source", bitmapSourceCCResults);
                                mismatchedCCResults.AddRange(MatchCrossCorrelationResults("bitmap_raw", bitmapRawCCResults, "bitmap_source", bitmapSourceCCResults));

                                WriteOutputHtml(bitmapSourceCCResults, mismatchedCCResults, bitmapSourceHash, sw, di, fi);
                            }
                        }
                    }
                    collectHashes = false;
                }
                sw.WriteLine("</body></html>");
            }

            Process.Start("result.html");

            Console.WriteLine("Hit any key to exit..");
            Console.ReadKey();
        }

        private static List<KeyValuePair<string, double>> CrossCorrelateDigests(Dictionary<string, Digest> hashes, Digest againstHash)
        {
            return hashes
                .Select(kv => new KeyValuePair<string, double>(kv.Key, ImagePhash.GetCrossCorrelation(againstHash, kv.Value)))
                .OrderBy(_ => _.Value)
                .ToList();
        }

        private static List<string> MatchCrossCorrelationResults(string hashCollectionNameOne, 
            IEnumerable<KeyValuePair<string, double>> hashesOne, string hashCollectionNameTwo,
            IEnumerable<KeyValuePair<string, double>> hashesTwo)
        {
            List<string> Mismatches = new List<string>();
            const double EqualityThreshold = 0.000000000000001;
            var enumeratorOne = hashesOne.GetEnumerator();
            var enumeratorTwo = hashesTwo.GetEnumerator();
            int length = 0;
            while (enumeratorOne.MoveNext() && enumeratorTwo.MoveNext())
            {
                if (!enumeratorOne.Current.Key.Equals(enumeratorTwo.Current.Key) ||
                    Math.Abs(enumeratorOne.Current.Value - enumeratorTwo.Current.Value) >= EqualityThreshold)
                {
                    Mismatches.Add($"Cross Correlation Result Mismatch: {hashCollectionNameOne}({enumeratorOne.Current.Key},{enumeratorOne.Current.Value}), {hashCollectionNameTwo}({enumeratorTwo.Current.Key},{enumeratorTwo.Current.Value})");
                }
                length++;
            }

            int overLength = length;
            while (enumeratorOne.MoveNext() || enumeratorTwo.MoveNext())
                overLength++;
            if (length != overLength)
                Mismatches.Add($"Mismatched Match Counter {length}, {overLength}");

            return Mismatches;
        } 

        private static void WriteOutputHtml(IEnumerable<KeyValuePair<string,double>> crossCorrelationResults, List<string> crossCorrelationMismatches, Digest againstHash, StreamWriter sw, DirectoryInfo di, FileInfo fi)
        {
            sw.Write("<h2>");
            sw.Write(di.Name);
            sw.Write("/");
            sw.Write(fi.Name);
            sw.WriteLine("</h2>");

            sw.Write("<img src=\"");
            sw.Write(new Uri(fi.FullName));
            sw.WriteLine("\" />");

            sw.Write("<p>");
            sw.Write(againstHash.ToString());
            sw.WriteLine("</p>");

            foreach (var mismatch in crossCorrelationMismatches)
            {
                Console.WriteLine(mismatch);
                sw.Write("<p>");
                sw.Write(mismatch);
                sw.WriteLine("</p>");
            }

            foreach (var result in crossCorrelationResults)
            {
                var key = result.Key;
                var D = result.Value;
                Console.WriteLine(" - - {0}: {1}", Path.GetFileName(key), D);

                sw.Write("<img width='64' height='64' src=\"");
                sw.Write(new Uri(key));
                sw.WriteLine("\" />");

                sw.WriteLine(D);
            }
            
        }
    }
}
