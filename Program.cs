using Azure.Storage.Blobs;
using Lab5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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

            app.MapRazorPages();

            /*app.MapGet("/Pages/Predictions/Create", () => "Display Create Page");
            app.MapGet("/Pages/Predictions/Delete", () => "Display Delete Page");
            app.MapGet("/Pages/Predictions/Index", () => "Display Index Page");*/

            app.Run();
        }
    }
}