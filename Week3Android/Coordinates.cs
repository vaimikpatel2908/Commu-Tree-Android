using System.Collections.Generic;

namespace Week3Android
{
    public class Coordinates
    {
        public string address { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        List<Coordinates> coordinates = new List<Coordinates>();

        public Coordinates()
        {
            this.coordinates = getCoordinates();
        }

        public Coordinates(string address, string latitude, string longitude)
        {
            this.address = address;
            this.latitude = latitude;
            this.longitude = longitude;
            this.coordinates = getCoordinates();
        }


        public List<Coordinates> getCoordinates()
        {
            List<Coordinates> list = new List<Coordinates>();
            list.Add(new Coordinates("1 Lee Centre Drive", "43.780963", "-79.245842"));
            list.Add(new Coordinates("25 Bay Mills Boulevard", "43.780397", "-79.304131"));
            list.Add(new Coordinates("1399 Victoria Park Avenue", "43.780963", "-79.29591"));
            list.Add(new Coordinates("265 Danforth Avenue", "43.676889", "-79.354626"));
            list.Add(new Coordinates("24 Parkway Forest Drive", " 43.769508", "-79.343262"));

            return list;
        }

        //co-ordinates
        //1.1 Lee Centre Drive, Toronto, ON M1H 1H9, Canada
        //Latitude: 43.780963 | Longitude: -79.245842
        //2. 25 Bay Mills Boulevard, Toronto, ON M1T 3K7, Canada
        //Latitude: 43.780397 | Longitude: -79.304131
        //3. 1399 Victoria Park Avenue, Toronto, ON M4B 2J0, Canada
        //Latitude: 43.708782 | Longitude: -79.29591
        //4. 265 Danforth Avenue, Toronto, ON M4K 1N1, Canada
        //Latitude: 43.676889 | Longitude: -79.354626
        //5. 24 Parkway Forest Drive, Toronto, ON M2J 1M4, Canada
        //Latitude: 43.769508 | Longitude: -79.343262

    }
}