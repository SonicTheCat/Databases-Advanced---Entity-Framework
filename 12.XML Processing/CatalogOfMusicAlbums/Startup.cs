namespace CatalogOfMusicAlbums
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    public class Startup
    {
        public static void Main()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Artist[]), new XmlRootAttribute("music"));
            var namespaces = new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });

            using (TextReader reader = new StreamReader("../../../catalog.xml"))
            {
                var artists = (Artist[])serializer.Deserialize(reader); 
            }
        }
    }
}