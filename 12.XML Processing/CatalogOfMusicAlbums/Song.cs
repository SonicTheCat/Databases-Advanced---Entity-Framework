using System.Xml.Serialization;

namespace CatalogOfMusicAlbums
{
    [XmlType("song")]
    public class Song
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("length")]
        public string Length { get; set; }
    }
}