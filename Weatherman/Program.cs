using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Weatherman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("What is your name?");
            //var userName = Console.ReadLine();
            //Console.WriteLine("");
            //Console.WriteLine($"Welcome {userName}, please enter a zipcode and I will give you the temp");
            //var zipCode = Console.ReadLine();

            var url = "http://api.openweathermap.org/data/2.5/weather?zip=33647,us&id=524901&APPID=ca27a9311ef37c705b0e639ecdfb29a6";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();

            var rawResponse = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawResponse = reader.ReadToEnd();
                Console.WriteLine(rawResponse);
            }



        }
    }
}
