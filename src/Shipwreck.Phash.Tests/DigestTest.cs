using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Shipwreck.Phash
{
    [TestClass]
    public class DigestTest
    {
        [TestMethod]
        public void SerializationTest()
        {
            var d = new Digest();

            for (var i = 0; i < d.Coefficents.Length; i++)
            {
                d.Coefficents[i] = (byte)(11 * i);
            }

            var xs = new XmlSerializer(d.GetType());

            using (var sw = new StringWriter())
            {
                xs.Serialize(sw, d);
                var xml = sw.ToString();

                using (var sr = new StringReader(xml))
                {
                    var d2 = (Digest)xs.Deserialize(sr);

                    CollectionAssert.AreEqual(d.Coefficents, d2.Coefficents);
                }
            }
        }
    }
}