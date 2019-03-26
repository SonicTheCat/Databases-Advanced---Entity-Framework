using System.Xml.Serialization;

namespace CatalogOfMusicAlbums
{
    [XmlType("album")]
    public class Album
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlArrayItem("song")]
        public Song[] Songs { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}