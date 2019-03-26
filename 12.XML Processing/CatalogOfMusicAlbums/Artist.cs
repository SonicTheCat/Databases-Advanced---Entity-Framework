namespace CatalogOfMusicAlbums
{
    using System.Xml.Serialization;

    [XmlType("artist")]
    public class Artist
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlArrayItem("album")]
        public Album[] Albums{ get; set; }
    }
}