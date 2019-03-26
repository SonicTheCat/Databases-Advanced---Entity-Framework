namespace CarDealer.Dtos
{
    using System.Xml.Serialization;

    [XmlType("supplier")]
    public class ImportSupplierDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }
    }
}