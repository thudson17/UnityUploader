using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UnityUploader.Services
{
    /// <summary>
    /// Global Configuration Class Wrapper (.net core config is ass)
    /// </summary>
    public class GlobalConfig
    {
        public static IConfiguration Configuration { get; set; }
        public static String getKeyValue(string key)
        {
            //use terrible .net core config API to get path to json file we write to
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetSection("AppConfiguration")[key];
        }

    }
}
