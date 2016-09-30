using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestGPS
{
    public partial class MainPage : ContentPage
    {
        private ILocationService _locationProvider;
        private ObservableCollection<UnivLocation> _data;
        private Boolean _gpsrecording = true;

        public MainPage()
        {
            InitializeComponent();
           
            _locationProvider = DependencyService.Get<ILocationService>();
            _data = new ObservableCollection<UnivLocation>();
            //_data.Add(new UnivLocation { Datum = DateTime.Now, Lat = 0, Lon = 0}); //DEBUG EMPTY ITEM
            lv.ItemsSource = _data;

            MessagingCenter.Subscribe<ILocationService, UnivLocation>(this, Messaging.LocationUpdated, HandleLocationUpdate);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _locationProvider.Start();
        }

        private void HandleLocationUpdate(ILocationService service, UnivLocation newLocation)
        {
            if (_gpsrecording)
                Device.BeginInvokeOnMainThread(() =>
                     _data.Insert(0, newLocation)
                );
        }

        private async void btnKlik(object sender, EventArgs e)
        {
            _gpsrecording = false;
             await SaveTodoItemAsync(_data.ToArray());
            _gpsrecording = true;
        }

        private void btnKlik2(object sender, EventArgs e)
        {
            _gpsrecording = false;
            _data.Clear();
            _gpsrecording = true;
        }

        public async Task<string> SaveTodoItemAsync(UnivLocation[] items)
        {
            using (var client = new HttpClient())
            {
                var RestUrl = ""; //nahradit s url serveru 
                var uri = new Uri(RestUrl);
                var json = JsonConvert.SerializeObject(items);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, content);
                var status = response.StatusCode;
                var respCont = response.Content.ReadAsStringAsync().Result;
                return respCont;
            }
        }
    }
}
