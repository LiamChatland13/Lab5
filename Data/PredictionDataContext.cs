using Lab5.Models; 
using Microsoft.EntityFrameworkCore;

namespace Lab5.Data
{
    public class PredictionDataContext : DbContext
    {
        public DbSet<Prediction> Predictions { get; set; }

        public PredictionDataContext(DbContextOptions<PredictionDataContext> options) : base(options)
        {
        }
    }
}
