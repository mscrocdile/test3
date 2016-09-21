using System;

namespace TestGPS
{
    public class UnivLocation
    {
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string Name { get; set; }

        public string GetLocation()
        {
            return (Lat == null) ? "Cannot tentononc" : string.Format("{0}, {1}", Lat, Lon);
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