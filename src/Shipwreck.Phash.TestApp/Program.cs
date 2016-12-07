using Shipwreck.Phash;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash.TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirs = new string[] { "compr", "blur", "rotd", "misc" };

            var hashes = new Dictionary<string, ulong>();
            using (var sw = new StreamWriter("result.html"))
            {
                sw.WriteLine("<!DOCTYPE html>");
                sw.WriteLine("<html><body>");
                var i = 0;
                foreach (var d in dirs)
                {
                    var di = new DirectoryInfo(d);
                    Console.WriteLine("{0}", di.FullName);

                    foreach (var fi in di.EnumerateFiles())
                    {
                        using (var fs = fi.OpenRead())
                        {
                            var hash = pHash.ph_dct_imagehash(fs);

                            Console.WriteLine(" - {0}: {1:X16}", fi.Name, hash);

                            if (i == 0)
                            {
                                hashes[fi.FullName] = hash;
                            }
                            else
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
                                sw.Write(hash.ToString("X16"));
                                sw.WriteLine("</p>");

                                foreach (var m in hashes.Select(kv => new { kv.Key, kv.Value, D = pHash.ph_hamming_distance(hash, kv.Value) }).OrderBy(_ => _.D))
                                {
                                    Console.WriteLine(" - - {0}: {1:D2}", Path.GetFileName(m.Key), m.D);

                                    sw.Write("<img width='64' height='64' src=\"");
                                    sw.Write(new Uri(m.Key));
                                    sw.WriteLine("\" />");

                                    sw.WriteLine(m.D);
                                }
                            }
                        }
                    }
                    i++;
                }
                sw.WriteLine("</body></html>");
            }

            Process.Start("result.html");

            Console.WriteLine("Hit any key to exit..");
            Console.ReadKey();
        }
    }
}
