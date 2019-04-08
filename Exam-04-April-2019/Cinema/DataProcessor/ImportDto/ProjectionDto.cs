namespace Cinema.DataProcessor.ImportDto
{
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Serialization;

    [XmlType("Projection")]
    public class ProjectionDto
    {
        [Required]     
        public int MovieId { get; set; }
        
        [Required]
        public int HallId { get; set; }  

        [Required]
        public string DateTime { get; set; }
    }
}