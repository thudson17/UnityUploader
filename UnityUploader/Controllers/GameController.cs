using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UnityUploader.Data;
using UnityUploader.Models;
using UnityUploader.Services;

namespace UnityUploader.Controllers
{
    public class GameController : Controller
    {
        private JSONDataManager dm = new JSONDataManager();

        //home page (list playable games via partial view)
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //list all games..
        [AllowAnonymous]
        public IActionResult List()
        {
            List<Game> Model = dm.LoadGameList().OrderBy(l => l.Name).ToList();
            return PartialView(Model);
        }


        //Create games (needs auth check..)
        [Authorize]
        public IActionResult Create()
        {
            VM_Game Model = new VM_Game();
            Model.FilePath = GlobalConfig.getKeyValue("DeployPathRoot");
            return View(Model);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(VM_Game Model)
        {

            //on invalid model just kick back to form
            if (!ModelState.IsValid)
                return View(Model);

            try
            { 

            //map our game vModel to Game Model (for JSON serialization)
            Game GameData = new Game();
            GameData.FilePath = Model.FilePath;
            GameData.Name = Model.Name;

                if (Model.UploadedZipFile != null)
                {
                    //copy uploaded file to temp zip file location
                    string FileName_Temp = GlobalConfig.getKeyValue("ZIPDumpsPath") + Guid.NewGuid().ToString() + ".zip";
                    FileStream fs = new FileStream(FileName_Temp, FileMode.Create);
                    Model.UploadedZipFile.CopyTo(fs);
                    fs.Close();


                    //extract temp file on target
                    ZipFile.ExtractToDirectory(FileName_Temp, Model.FilePath);

                    //cleanup the temp zip file
                    System.IO.File.Delete(FileName_Temp);

                    return RedirectToAction("Index");
                }


            }
            catch (Exception ex)
            {
                //todo: cleanup this logging process
                ViewData["ServerMessage"] = ex.Message;
            }


            return View(Model);
        }
    }
}