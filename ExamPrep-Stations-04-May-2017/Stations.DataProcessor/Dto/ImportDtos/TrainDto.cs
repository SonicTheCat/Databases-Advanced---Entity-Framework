namespace Stations.DataProcessor.Dto.ImportDtos
{
    using Stations.Models.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TrainDto
    {
        [MaxLength(10)]
        [Required]
        public string TrainNumber { get; set; }

        public TrainType? Type { get; set; } = TrainType.HighSpeed;

        public IList<TrainSeatDto> Seats { get; set; } = new List<TrainSeatDto>();
    }
}