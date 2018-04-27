using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnityUploader.Models
{
    public class vmGame : PageModel
    {
        public string name;
        public string filePath;
        public string webPath;
      
        public IFormFile FileUpload;


        public async Task OnGetAsync()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            //FileUpload is null
            return RedirectToPage("/Home/Index");
        }

    }
}
