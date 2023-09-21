using GMap.NET;
using GMap.NET.ObjectModel;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static GMap.NET.Entity.OpenStreetMapGraphHopperGeocodeEntity;

namespace Task_1
{
    public partial class MainForm : Form
    {
        private Size _marginSize;
        private GMapOverlay _markersOverlay;
        private ObservableCollectionThreadSafe<Marker> _markersValues;
        private MarkerDatabase _dataBase;
        public MainForm()
        {
            _dataBase = new MarkerDatabase();
            _markersValues = new ObservableCollectionThreadSafe<Marker>();
            InitializeComponent();
        }
        private async void gMapControlMain_Load(object sender, EventArgs e)
        {
            if (sender is MainForm mainForm)
                _marginSize = mainForm.Size - gMapControlMain.Size;

            _markersOverlay = new GMapOverlay("markers");
            gMapControlMain.Overlays.Add(_markersOverlay);

            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControlMain.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            gMapControlMain.MinZoom = 2;
            gMapControlMain.MaxZoom = 16;
            gMapControlMain.Zoom = 4;
            gMapControlMain.Position = new PointLatLng(54.59568572, 82.57211176); // Russia, Novosibirsk
            gMapControlMain.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            gMapControlMain.CanDragMap = true;
            gMapControlMain.DragButton = MouseButtons.Left;
            gMapControlMain.ShowCenter = false;
            gMapControlMain.ShowTileGridLines = false;

            _markersValues.CollectionChanged += _markersValues_CollectionChanged;

            foreach (Marker item in await _dataBase.GetAllMarkersAsync())
                _markersValues.Add(item);
        }

        private void _markersValues_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems?[0] is Marker newMarker)
                    {
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(newMarker.Lat, newMarker.Lng), GMarkerGoogleType.blue_pushpin);
                        _markersOverlay.Markers.Add(marker);
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.NewItems?[0] is Marker replacingMarker && e.OldItems?[0] is Marker replacedMarker)
                    {
                        PointLatLng point = new PointLatLng(replacingMarker.Lat, replacingMarker.Lng);
                        _markersOverlay.Markers.Skip(e.NewStartingIndex).First().Position = point;
                    }
                    break;
            }
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (sender is MainForm mainForm)
                gMapControlMain.Size = mainForm.Size - _marginSize;
        }

        private async void gMapControlMain_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private async void gMapControlMain_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            int index = _markersOverlay.Markers.IndexOf(item);
            Marker newMarker = new Marker(_markersValues[index]);
            newMarker.Lng += 3;
            _markersValues[index] = newMarker;
            await _dataBase.UpdateMarkerAsync(newMarker);
        }
    }
}
