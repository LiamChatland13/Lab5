using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{

    public enum Question
    {
        Earth,
        Computer
    }
    public class Prediction
    {
        [Required]
        public int PredictionId { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string FileName { get; set; }
        [StringLength(50, MinimumLength = 1)]
        [Required]
        public string Url { get; set; }
        [Required]
        public Question Question { get; set; }
        public byte[] Image { get; set; }








    }
}
