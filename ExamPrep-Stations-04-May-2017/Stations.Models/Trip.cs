namespace Stations.Models
{
    using Stations.Models.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Trip
    {
        public int Id { get; set; }

        [ForeignKey(nameof(OriginStation))]
        public int OriginStationId { get; set; }
        [Required]
        public Station OriginStation { get; set; }

        [ForeignKey(nameof(DestinationStation))]
        public int DestinationStationId { get; set; }
        [Required]
        public Station DestinationStation { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }
        [Required]
        public Train Train { get; set; }

        public TripStatus Status { get; set; } = TripStatus.OnTime;

        public TimeSpan? TimeDifference { get; set; }
    }
}