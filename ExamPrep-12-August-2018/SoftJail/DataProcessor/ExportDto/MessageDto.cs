namespace SoftJail.DataProcessor.ExportDto
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Message")]
    public class MessageDto
    {
        private string description;

        [XmlElement()]
        public string Description
        {
            get
            {
                return this.description; 
            }
            set
            {
                Array.Reverse(value.ToCharArray());

                description = value; 
            }
        }
    }
}