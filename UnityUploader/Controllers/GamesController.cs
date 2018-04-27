using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnityUploader.Data;
using UnityUploader.Models;

namespace UnityUploader.Controllers
{
    public class GamesController : Controller
    {

        private JSONDataManager dm = new JSONDataManager();


        //default list of playable game
        public IActionResult Index()
        {
            return View();
        }

        //list all games
        public IActionResult pv_ListGames()
        {
            return PartialView();
        }

        //play a game
        public IActionResult Play(int GameID)
        {
            return View();
        }


        public IActionResult Create()
        {
            vmGame model = new vmGame();
            model.filePath = @"C:\\inetpub\\wwwroot\\";
            model.webPath = @"tomhudson.website\games\";
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(vmGame vModel)
        {

            Game Model = new Game();
            Model.name = vModel.name;
            Model.filePath = vModel.filePath;
            Model.webPath = vModel.webPath;

            if (Model != null)
            {
               List<Game> Games = dm.LoadGameList();
             
                if (Games == null)
                    Games = new List<Game>();

                //hande file upload
               if (vModel.FileUpload != null)
                {

                    //copy this to a local directory on server
                    FileStream fs = new FileStream(Model.filePath, FileMode.Create);
                    string FileName_Temp = dm.getZIPDumpsPath() + Guid.NewGuid().ToString();

                    //copy http file to temp
                    vModel.FileUpload.CopyTo(fs);

                    //extract temp file on target
                    ZipFile.ExtractToDirectory(FileName_Temp, Model.filePath);

                    System.IO.File.Delete(FileName_Temp);

                }

                Games.Add(Model);
                dm.SaveGameList(Games);


            }

            return View();
        }




    }
}