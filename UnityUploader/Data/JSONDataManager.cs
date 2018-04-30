using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityUploader.Models;
using UnityUploader.Services;

namespace UnityUploader.Data
{

    //used to save/load stuff to a simple flat json file rather than messing with a db

    public class JSONDataManager
    {
        private string _jsonFilePath;

        public JSONDataManager()
        {
            this._jsonFilePath = GlobalConfig.getKeyValue("JSONDataPath");
        }

        /// <summary>
        /// Save JSON version of game list to files
        /// </summary>
        /// <param name="JSON"></param>
        public void SaveGameList(List<Game> Games)
        {
            System.IO.File.WriteAllText(this._jsonFilePath, JsonConvert.SerializeObject(Games));
        }


        /// <summary>
        /// Loads List of Games from stored JSON File
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public List<Game> LoadGameList()
        {
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(this._jsonFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                List<Game> games = (List<Game>)serializer.Deserialize(file, typeof(List<Game>));

                return games;

            }
        }

        /// <summary>
        /// Loads List of Games from stored JSON File (filtered by specified game name)
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public Game LoadGame(string Name)
        {
            return LoadGameList().Where(g => g.Name == Name).FirstOrDefault();
        }



    }
}
