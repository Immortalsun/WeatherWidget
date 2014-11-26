using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using WeatherWidget.ViewModel;

namespace WeatherWidget.APIConnect
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
        private readonly object padlock = new object(); //locking mechanism
        private Dictionary<WeatherViewModel,string> WeatherDictionary; 
        private volatile bool Updating;
        #endregion

        #region Properties
        public bool UpdateSuccessful { get; set; }
        #endregion

        #region Constructor

        public WeatherRequester()
        {
            WeatherDictionary = new Dictionary<WeatherViewModel,string>();
            updateTime = new TimeSpan(0,0,15,0);
        }


        #endregion

        #region Methods
        /// <summary>
        /// Starts weather updates
        /// </summary>
        public async void Start()
        {
            Updating = true;
            while (Updating)
            {
                await UpdateWeather();
                if (UpdateSuccessful)
                {
                    lock (padlock)
                    {
                        Monitor.Wait(padlock, updateTime);
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Stops weather updates
        /// </summary>
        public void Stop()
        {
            Updating = false;

            lock (padlock)
            {
                Monitor.Pulse(padlock);
            }
        }


        /// <summary>
        /// Figure out if there is anything
        /// in the weather dictionary
        /// </summary>
        /// <returns>True if there are any items</returns>
        public bool Any()
        {
            return WeatherDictionary.Any();
        }

        /// <summary>
        /// Adds a view model to the Requester
        /// </summary>
        /// <param name="model">The WeatherViewModel to add</param>
        private void AddViewModel(WeatherViewModel model)
        {
            WeatherDictionary.Add(model, GenerateRequestString(model.CityName));
        }

        private void RemoveViewModel(WeatherViewModel model)
        {
            WeatherDictionary.Remove(model);
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
            UpdateSuccessful = false;
            //if there are any
            if (WeatherDictionary.Any())
            {
                //for each url
                foreach (var kvp in WeatherDictionary)
                {
                    //asynchronously request the weather api
                    XmlDocument content = await GetWeatherUpdateFromWebAsyc(kvp.Value);
                    if (content != null)
                    {
                        //pass the xml along to the view model for parsing and updating
                        var viewModel = kvp.Key;
                        viewModel.UpdateWeather(content);
                    }
                }
                UpdateSuccessful = true;
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
        /// <summary>
        /// This event is attached in the WeatherCollectionViewModel
        /// It automatically updates the requester when new view models are added
        /// or removed from the WeatherCollectionViewModel's collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WeatherItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action.Equals(NotifyCollectionChangedAction.Add))
            {
                if (e.NewItems != null && e.NewItems.Count > 0)
                {
                    foreach (WeatherViewModel model in e.NewItems)
                    {
                        AddViewModel(model);
                    }
                }
            }
            else if (e.Action.Equals(NotifyCollectionChangedAction.Remove))
            {
                if (e.OldItems != null && e.OldItems.Count > 0)
                {
                    foreach (WeatherViewModel model in e.OldItems)
                    {
                        RemoveViewModel(model);
                    }
                }
            }
        }
        #endregion

    }
}
