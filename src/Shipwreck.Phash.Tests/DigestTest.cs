using System.IO;
using System.Xml.Serialization;
using Xunit;

namespace Shipwreck.Phash
{
    public class DigestTest
    {
        [Fact]
        public void SerializationTest()
        {
            var d = new Digest();

            for (var i = 0; i < d.Coefficients.Length; i++)
            {
                d.Coefficients[i] = (byte)(11 * i);
            }

            var xs = new XmlSerializer(d.GetType());

            using (var sw = new StringWriter())
            {
                xs.Serialize(sw, d);
                var xml = sw.ToString();

                using (var sr = new StringReader(xml))
                {
                    var d2 = (Digest)xs.Deserialize(sr);

                    Assert.Equal(d.Coefficients, d2.Coefficients);
                }
            }
        }
    }
}