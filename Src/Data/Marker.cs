using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    internal class Marker
    {
        public Marker(int id, double lat, double lng) { Id = id; Lat = lat; Lng = lng; }
        public Marker(Marker marker) { Id = marker.Id; Lat = marker.Lat; Lng = marker.Lng; }
        public int Id { get; }
        public double Lat { get; set; }
        public double Lng { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
