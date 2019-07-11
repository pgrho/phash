namespace Shipwreck.Phash
{
    /// <summary>
    /// feature vector info
    /// </summary>
    internal class Features
    {
        public Features(int length)
        {
            Items = new double[length];
        }

        public double[] Items { get; }

        public double[] features => Items;
    }
}