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

        //home page (list playable games, view components)
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        //play the selected game
        public IActionResult Play(string Name)
        {
            ViewBag.WebPath = GlobalConfig.getKeyValue("WebPath") + Name;
            return View();
          //  return Redirect(GlobalConfig.getKeyValue("WebPath") + Name); <-- this return full redirect to game above is iframe wrapper
        }



        //Create games (needs auth check..)
        [Authorize]
        public IActionResult Create()
        {
            VM_Game Model = new VM_Game();
            Model.FilePath = GlobalConfig.getKeyValue("DeployPathRoot");
            return View(Model);
        }

        //delete game
        [Authorize]
        [HttpGet]
        public IActionResult Delete(String Name)
        {
            //find game to delete in json (just use name)
            var games = dm.LoadGameList();
            var game_to_remove = games.Where(g => g.Name == Name).FirstOrDefault();

            if (game_to_remove != null)
            {
                //delete file contents
                if (System.IO.File.Exists(game_to_remove.FilePath))
                    System.IO.File.Delete(game_to_remove.FilePath);

                //remove game from json
                games.Remove(game_to_remove);
                dm.SaveGameList(games);
            }


            return RedirectToAction("Index");
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


                }

                //save the new game to our json file
                var modified_game_list = dm.LoadGameList();
                modified_game_list.Add(GameData);
                dm.SaveGameList(modified_game_list);


                return RedirectToAction("Index");

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