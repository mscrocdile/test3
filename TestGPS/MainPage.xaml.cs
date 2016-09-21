using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace TestGPS
{
    public partial class MainPage : ContentPage
    {
        private ILocationService _locationProvider;
        private ObservableCollection<UnivLocation> _data;

        public MainPage()
        {
            InitializeComponent();
           
            _locationProvider = DependencyService.Get<ILocationService>();
            _data = new ObservableCollection<UnivLocation>();
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
            Device.BeginInvokeOnMainThread(() =>
                 _data.Insert(0, newLocation)
            );
        }

        private void btnKlik(object sender, EventArgs e)
        {
           // MessagingCenter.Send<ILocationService, UnivLocation>(new FooLoc(), Messaging.LocationUpdated, new UnivLocation { Lat=5.3,Lon=5.3, Name = "Just test" });
        }
    }
}
