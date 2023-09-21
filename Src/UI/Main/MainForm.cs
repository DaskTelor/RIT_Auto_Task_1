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

namespace Task_1
{
    public partial class MainForm : Form
    {
        private Size _marginSize;
        private MainViewModel _viewModel;
        private GMapOverlay _markersOverlay;
        private GMapMarker _targetMarkerDragDrop;
        private bool _dragStatusActive = false;

        public MainForm(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
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
            gMapControlMain.DragButton = MouseButtons.Right;
            gMapControlMain.ShowCenter = false;
            gMapControlMain.ShowTileGridLines = false;

            _viewModel.AddMarkersCollectionChangedEventHandler(markersValues_CollectionChanged);
            await _viewModel.LoadAllMarkerAsync();
        }

        private void markersValues_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
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

        private void gMapControlMain_OnMarkerEnter(GMapMarker item)
        {
            if (!_dragStatusActive)
            {
                _targetMarkerDragDrop = item;
            } else
            {
                gMapControlMain.Cursor = Cursors.Hand;
            }
        }

        private void gMapControlMain_OnMarkerLeave(GMapMarker item)
        {
            if (!_dragStatusActive && item.Equals(_targetMarkerDragDrop))
            {
                _targetMarkerDragDrop = null;
            }
            if (_dragStatusActive)
            {
                gMapControlMain.Cursor = Cursors.Hand;
            }
        }

        private void gMapControlMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _targetMarkerDragDrop != null)
            {
                gMapControlMain.Cursor = Cursors.Hand;
                _dragStatusActive = true;
            }
        }

        private async void gMapControlMain_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left && _targetMarkerDragDrop != null && _dragStatusActive)
            {
                await _viewModel.UpdateMarker(
                    _markersOverlay.Markers.IndexOf(_targetMarkerDragDrop),
                    gMapControlMain.FromLocalToLatLng(e.X, e.Y));
                gMapControlMain.Cursor = Cursors.Default;
                _dragStatusActive = false;
            }
        }
    }
}
