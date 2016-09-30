using System;

namespace TestGPS
{
    public class UnivLocation
    {
        public int ID { get; set; }
        public string MobileID { get; set; }
        public DateTime Datum { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Info { get; set; }

        public string GetLocation()
        {
            return string.Format("{0}, {1}", Lat, Lon);
        }
    }

    public class FooLoc: ILocationService
    {
        public void Start()
        {
            //foo
        }
    }


    public interface ILocationService
	{
		void Start();
    }
}