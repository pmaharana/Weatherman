using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            var localDate = DateTime.Now;
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

            var weatherResult = JsonConvert.DeserializeObject<RootObject>(rawResponse);
            Console.WriteLine($"Your name: {userName} Your location: {weatherResult.name} and your temperature: {weatherResult.main.temp} deg F");
            Console.WriteLine($"The weather conditions: {weatherResult.weather[0].main} at {localDate}" );

            const string connectionString =
                @"Server=localhost\SQLEXPRESS;Database=Weatherman;Trusted_Connection=True;";






            using (var connection = new SqlConnection(connectionString))
            {

                var text = @"INSERT INTO Weatherman (Temperature, Conditions, Name)
                            Values (@Temperature, @Conditions, @Name)"
                        ;

                var cmd = new SqlCommand(text, connection);

                cmd.Parameters.AddWithValue("@Temperature", weatherResult.main.temp);
                cmd.Parameters.AddWithValue("@Conditions", weatherResult.weather[0].main);
                cmd.Parameters.AddWithValue("@Name", userName);
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

}



