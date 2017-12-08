using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Shipwreck.Phash.Bitmaps;

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
                        BitmapHash = BitmapPhash.ComputeBitmapDigest(bitmap);
                        RawBitmapHash = BitmapPhash.ComputeRawBitmapDigest(bitmap.ToRawBitmapData());
                    }
                    fs.Position = 0;
                    BitmapSourceHash = ImagePhash.ComputeDigest(fs);

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
            var prg = new Program();

            var targets = new List<string>();

            foreach (var a in args)
            {
                // TODO: parse arg and continue

                targets.Add(a);
            }

            prg.ProcessFiles(targets.Any() ? targets
                            : new string[] { "compr", "blur", "rotd", "misc" }.SelectMany(d => Directory.EnumerateFiles(d)));
        }

        private void ProcessFiles(IEnumerable<string> fs)
        {
            var files = fs.Select(f => Path.GetFullPath(f).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar)).ToList();

            var digests = files.AsParallel().Select(f => new FileDigests(f)).ToList();
            var results = digests.SelectMany((d1, i) => digests.Skip(i).Select((d2, j) => new CCR
            {
                i = i,
                j = j + i,
                m = ImagePhash.GetCrossCorrelation(d1.BitmapSourceHash, d2.BitmapSourceHash)
            })).OrderBy(v => v.m).ToList();

            var bd = GetCommonPath(files);

            OutputToConsole(bd, digests, results);
        }

        #region Output result

        private static void OutputToConsole(string bd, List<FileDigests> digests, List<CCR> results)
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
            Console.WriteLine("Hit any key to exit..");
            Console.ReadKey();
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