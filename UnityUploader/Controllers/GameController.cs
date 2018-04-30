using System;
using System.Collections.Generic;
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

        //list all games..
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
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
            return View(Model);
        }
    }
}