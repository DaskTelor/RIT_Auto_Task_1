using GMap.NET;
using GMap.NET.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    public class MainViewModel
    {
        private ObservableCollectionThreadSafe<Marker> _markersValues;
        private MarkerDatabase _dataBase;

        public MainViewModel(MarkerDatabase markerDatabase) 
        {
            _dataBase = markerDatabase;
            _markersValues = new ObservableCollectionThreadSafe<Marker>();
        }

        public void AddMarkersCollectionChangedEventHandler(NotifyCollectionChangedEventHandler action)
        {
            _markersValues.CollectionChanged += action;
        }

        public async Task UpdateMarker(int index, PointLatLng point)
        {
            Marker newMarker = new Marker(_markersValues[index]);

            newMarker.Lat = point.Lat;
            newMarker.Lng = point.Lng;
            _markersValues[index] = newMarker;
            await _dataBase.UpdateMarkerAsync(newMarker);
        }

        public async Task LoadAllMarkerAsync()
        {
            foreach (Marker item in await _dataBase.GetAllMarkersAsync())
                _markersValues.Add(item);
        }


    }
}
