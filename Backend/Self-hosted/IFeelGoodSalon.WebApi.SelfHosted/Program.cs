using IFeelGoodSalon.WebApi;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.WebApi.SelfHosted
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            using (WebApp.Start<Startup>(baseUrl))
            {
                Console.WriteLine("RiverAquarium Server is listening on {0}", baseUrl);
                Console.ReadKey(true);
            }
        }
    }
}
