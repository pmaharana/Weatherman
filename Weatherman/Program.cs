using Newtonsoft.Json;
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
            var localDate = DateTime.Now.ToString("HH:mm:ss tt");
            Console.WriteLine("What is your name?");
            var userName = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine($"Welcome {userName}, please enter a zipcode");
            var zipCode = Console.ReadLine();
            Console.WriteLine($"Thank you, {userName}, please enter a country code eg. us");
            var countryCode = Console.ReadLine();
            var url = "http://api.openweathermap.org/data/2.5/weather?zip=" + zipCode + "," + countryCode + "&units=imperial&id=524901&APPID=ca27a9311ef37c705b0e639ecdfb29a6";

            var request = WebRequest.Create(url);

            var response = request.GetResponse();

            var rawResponse = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawResponse = reader.ReadToEnd();
                
            }

            var dawsonRidge = JsonConvert.DeserializeObject<RootObject>(rawResponse);
            Console.WriteLine($"Your name: {userName} Your location: {dawsonRidge.name} and your temperature: {dawsonRidge.main.temp} deg F");
            Console.WriteLine($"The weather conditions: {dawsonRidge.weather[0].main} at {localDate}" );
        }
    }
}
