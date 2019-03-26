using System.Xml.Serialization;

namespace CarDealer.Dtos.ExerciseFourDto
{
    [XmlType("car")]
    public class CarDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance{ get; set; }

        [XmlArray("parts")]
        public PartDto[] Parts { get; set; }
    }
}
