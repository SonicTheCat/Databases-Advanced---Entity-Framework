namespace Stations.DataProcessor.Dto.ImportDtos
{
    using Stations.Models.Enums;
    using System.ComponentModel.DataAnnotations;

    public class TripDto
    {
        [MaxLength(10)]
        [Required]
        public string Train { get; set; }

        [Required]
        public string OriginStation { get; set; }

        [Required]
        public string DestinationStation { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        public TripStatus? Status { get; set; } = TripStatus.OnTime;

        public string TimeDifference { get; set; }
    }
}