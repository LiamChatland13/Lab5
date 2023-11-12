using Azure.Storage.Blobs;
using Lab5.Data;
using Microsoft.EntityFrameworkCore;

namespace Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var connection = builder.Configuration.GetConnectionString("DefaultDBConnection");
            builder.Services.AddDbContext<PredictionDataContext>(options => options.UseSqlServer(connection));

            var blobConnection = builder.Configuration.GetConnectionString("AzureBlobStorage");
            builder.Services.AddSingleton(new BlobServiceClient(blobConnection));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers(); // Add this line to enable MVC-style routing

            app.MapGet("/", () => "Hello, World!");

            app.Run();
        }
    }
}