using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using WeatherWidget.Model;

namespace WeatherWidget
{
    /// <summary>
    /// Class that handles requestin the online weather API for information
    /// </summary>
    public class WeatherRequester
    {

        #region Fields

        private const string WeatherRequestURL = "http://api.worldweatheronline.com/free/v2/weather.ashx?";
        private const string WeatherSearchURL = "http://api.worldweatheronline.com/free/v2/search.ashx?";
        private TimeSpan updateTime;

        private List<WeatherViewModel> WeatherList;
        private Dictionary<string, WeatherViewModel> WeatherDictionary; 
        private bool _updating;
        #endregion

        #region Properties

        #endregion

        #region Constructor

        public WeatherRequester()
        {
            WeatherList = new List<WeatherViewModel>();
            updateTime = new TimeSpan(0,0,15,0);
        }


        #endregion

        #region Methods
        /// <summary>
        /// Starts weather updates
        /// </summary>
        public async void Start()
        {
            while (_updating)
            {
                await UpdateWeather();
                Thread.Sleep(updateTime);
            }
        }

        /// <summary>
        /// Stops Weather updates
        /// </summary>
        public void Stop()
        {
            _updating = false;
            GC.Collect();
        }


       
        /// <summary>
        /// Loads a list of weather view model objects
        /// from a serialized xml file
        /// </summary>
        public void Load()
        {
        }

        /// <summary>
        /// Saves the list of current weather viwe model objects
        /// to a serialized xml file
        /// </summary>
        public void Save()
        {
        }

        public void AddViewModel(WeatherViewModel model)
        {
            Stop();
            WeatherDictionary.Add(GenerateRequestString(model.CityName), model);
            Start();
        }

        /// <summary>
        /// Generates a request call to the api 
        /// for the specified city name
        /// </summary>
        /// <param name="name">Name of city we are requesting</param>
        /// <returns></returns>
        public string GenerateRequestString(string name)
        {
            name = name.Trim();
            name = name.Replace(' ', '_');


            StringBuilder sb = new StringBuilder();
            sb.Append(WeatherRequestURL);
            sb.Append("key=");
            sb.Append(ApiKey.Key);
            sb.Append("&q=");
            sb.Append(name);
            sb.Append("&format=xml");

            return sb.ToString();
        }



        /// <summary>
        /// Updates the weather view model object
        /// </summary>
        /// <returns></returns>
        public async Task UpdateWeather()
        {

            //if there are any
            if (WeatherDictionary.Any())
            {
                //for each url
                foreach (var kvp in WeatherDictionary)
                {
                    //asynchronously request the weather api
                    XmlDocument content = await GetWeatherUpdateFromWebAsyc(kvp.Key);
                    //pass the xml along to the view model for parsing and updating
                    var viewModel = kvp.Value;
                    viewModel.UpdateWeather(content);
                }
            }

        }

        /// <summary>
        /// Asynchronous method that requests for
        /// weather data from the web api.
        /// </summary>
        /// <param name="requestUrl">Api Query url</param>
        /// <returns>The xml document containing the web data</returns>
        private async Task<XmlDocument> GetWeatherUpdateFromWebAsyc(string requestUrl)
        {
            //Initialize content variable for return
            var content = new XmlDocument();
            //Initialize memory stream for recieving web response data
            var memoryStream = new MemoryStream();
            //Create web request based on the url entered
            var webRequest = (HttpWebRequest) WebRequest.Create(requestUrl);
            //Query the rest api for weather data asychronously and await the response
            using (WebResponse response = await webRequest.GetResponseAsync())
            {
                //stream the response
                using (Stream responseStream = response.GetResponseStream())
                {
                    //copy asychronously into memory
                    await responseStream.CopyToAsync(memoryStream);
                }
            }

            using (memoryStream)
            {
                //set the memory stream position back to the beginning
                memoryStream.Position = 0;
                //create stream reader to get data
                var reader = new StreamReader(memoryStream);
                //get xml data string
                var xmlString = reader.ReadToEnd();
                //load the xml into the content document for parsing
                content.LoadXml(xmlString);
            }
            //close the memory stream
            memoryStream.Close();
            //return the xml document for parsing
            return content;
        }

        #endregion

        #region Events

        #endregion

    }
}
