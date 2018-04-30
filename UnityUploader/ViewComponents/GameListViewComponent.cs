using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityUploader.Data;

namespace UnityUploader.ViewComponents
{
    public class GameListViewComponent : ViewComponent
    {
        private JSONDataManager dm = new JSONDataManager();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var games = await Task.Run(() => dm.LoadGameList());
            return View(games);
        }


    }
}
