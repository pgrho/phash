using System;
using Shipwreck.Phash.V0_3_6.Imaging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Shipwreck.Phash.Tests.Compatibility")]
namespace Shipwreck.Phash.V0_3_6
{
    /// <summary>
    /// Radon Projection info
    /// </summary>
    public class Projections
    {
        public Projections(int regionWidth, int regionHeight, int lineCount)
        {
            Region = new Imaging.FloatImage(regionWidth, regionHeight);
            PixelsPerLine = new int[lineCount];
        }

        /// <summary>
        /// contains projections of image of angled lines through center
        /// </summary>
        public FloatImage Region { get; }

        /// <summary>
        /// int array denoting the number of pixels of each line
        /// </summary>
        public int[] PixelsPerLine { get; }
    }
}