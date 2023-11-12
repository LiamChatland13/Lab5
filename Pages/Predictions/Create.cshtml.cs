using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Lab5.Data;
using Lab5.Models;
using Azure.Storage.Blobs;

namespace Lab5.Pages.Predictions
{
    public class CreateModel : PageModel
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";

        private readonly Lab5.Data.PredictionDataContext _context;

        public CreateModel(Lab5.Data.PredictionDataContext context, BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Prediction Prediction { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient("your-container-name");

                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    var blobName = $"{Guid.NewGuid()}{Path.GetExtension(ImageFile.FileName)}";
                    var blobClient = containerClient.GetBlobClient(blobName);

                    await blobClient.UploadAsync(memoryStream, true);
                    Prediction.Url = blobClient.Uri.ToString();
                }
            }
            _context.Predictions.Add(Prediction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
