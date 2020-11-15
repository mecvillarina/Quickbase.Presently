using Presently.MobileApp.Common.Constants;
using Presently.MobileApp.Models;
using Presently.MobileApp.PubSubEvents;
using Prism.Events;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Presently.MobileApp.Views
{
    public partial class ClockPage : MobileContentPageBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SubscriptionToken _mapMoveToRegionSubscriptionEventToken;
        private readonly SubscriptionToken _mapSetPinSubscriptionEventToken;
        private Position _initialPosition;

        public ClockPage(IEventAggregator eventAggregator)
        {
            InitializeComponent();

            _eventAggregator = eventAggregator;
            _mapMoveToRegionSubscriptionEventToken = _eventAggregator.GetEvent<MapMoveToRegionEvent>().Subscribe((pos) => MoveToRegion(pos), ThreadOption.UIThread);
            _mapSetPinSubscriptionEventToken = _eventAggregator.GetEvent<MapSetPinEvent>().Subscribe((pos) => SetPin(pos), ThreadOption.UIThread);

            _initialPosition = new Position(13.6862045, 120.885412);
            map.MoveCamera(CameraUpdateFactory.NewPositionZoom(_initialPosition, 20));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(_initialPosition, Distance.FromKilometers(13)), false);

            map.UiSettings.ZoomControlsEnabled = false;
            map.UiSettings.CompassEnabled = false;
            map.UiSettings.MyLocationButtonEnabled = false;
            map.UiSettings.ScrollGesturesEnabled = true;
            map.UiSettings.ZoomGesturesEnabled = true;
            map.UiSettings.TiltGesturesEnabled = false;
            map.PinDragEnd += Map_PinDragEnd;
        }

        private void SetPin(MapSetPinModel pinModel)
        {
            var currentPin = map.Pins.FirstOrDefault(x => x.Tag == pinModel.Tag);

            var pos = new Position(pinModel.Latitude, pinModel.Longitude);
            if (currentPin == null)
            {
                currentPin = new Pin()
                {
                    Icon = BitmapDescriptorFactory.FromBundle(pinModel.PinSource),
                    Position = pos,
                    Label = "",
                    IsDraggable = pinModel.IsDraggable,
                    Tag = pinModel.Tag
                };
            }
            else
            {
                currentPin.Position = pos;
                map.Pins.Clear();
            }

            map.Pins.Add(currentPin);
        }

        private void MoveToRegion(MapMoveToRegionModel model)
        {
            var pos = new Position(model.Latitude, model.Longitude);

            map.MoveCamera(CameraUpdateFactory.NewPositionZoom(pos, 16));
            map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMeters(model.RadiusInMeters)));
        }

        private void Map_PinDragEnd(object sender, PinDragEventArgs e)
        {
            if (e.Pin.Tag.ToString() == MapTags.CurrentLocation)
            {
                _eventAggregator.GetEvent<MapDragPinNewLocationEvent>().Publish(new MapPositionModel(e.Pin.Position.Latitude, e.Pin.Position.Longitude));
            }
        }

        public void Destroy()
        {
            map.PinDragEnd -= Map_PinDragEnd;

            _eventAggregator.GetEvent<MapSetPinEvent>().Unsubscribe(_mapSetPinSubscriptionEventToken);
            _eventAggregator.GetEvent<MapMoveToRegionEvent>().Unsubscribe(_mapMoveToRegionSubscriptionEventToken);
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}
