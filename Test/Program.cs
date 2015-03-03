using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libHammer.Sql;
using System.Configuration;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            var results = SqlWrapper.Execute(ConfigurationManager.ConnectionStrings["aat_weather"].ConnectionString,
                "SELECT * from weather_sources");

            stopWatch.Stop();
            Console.WriteLine("Elapsed time: " + stopWatch.ElapsedMilliseconds);


        }
    }
}
