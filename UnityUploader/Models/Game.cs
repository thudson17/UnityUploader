using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnityUploader.Models
{
    //a unity game
    [Serializable]
    public class Game
    {
        public string Name { get; set; }
        public string FilePath { get; set; }

    }
}
