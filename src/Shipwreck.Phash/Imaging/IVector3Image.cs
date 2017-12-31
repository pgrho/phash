using System.Numerics;

namespace Shipwreck.Phash.Imaging
{
    public interface IVector3Image
    {
        int Width { get; }
        int Height { get; }

        /// <summary>
        /// Gets the RGB components of the specified pixel.
        /// </summary>
        /// <param name="x">The x position of the pixel.</param>
        /// <param name="y">The y position of the pixel.</param>
        /// <returns>The <see cref="Vector3"/> that represents the RGB component value. Each component is supposed to be within [0, 255].</returns>
        Vector3 this[int x, int y] { get; }
    }
}