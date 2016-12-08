using Shipwreck.Phash.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash
{
    /// <summary>
    /// feature vector info
    /// </summary>
    public class Features
    {
        public Features(int length)
        {
            Items = new double[length];
        }

        public double[] Items { get; }

        public double[] features => Items;
    }
}