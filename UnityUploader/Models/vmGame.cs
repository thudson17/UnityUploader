using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UnityUploader.Models
{
    //viewmodel for uploading game files. In .NET core, uploading files is more painful than going to the dentist
    [Serializable]
    public class VM_Game
    {
        [Required]
        public string Name { get; set; }
        [DisplayName("File Path (Inetpub)")]
        [Required]
        public string FilePath { get; set; }
        public IFormFile UploadedZipFile { get; set; }

    }
}
