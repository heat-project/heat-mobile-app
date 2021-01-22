using System;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.GoogleMaps.Android.Factories;
using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;
using AndroidBitmapDescriptorFactory = Android.Gms.Maps.Model.BitmapDescriptorFactory;


namespace HeatApp.Droid
{
    public sealed class BitmapConfig : IBitmapDescriptorFactory
    {
        public AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor)
        {
            int iconId = 0;
            switch (descriptor.Id)
            {
                case "user_marker":
                    iconId = Resource.Drawable.user_marker;
                    break;
                case "bus_station":
                    iconId = Resource.Drawable.bus_station;
                    break;
                case "small_green_bus":
                    iconId = Resource.Drawable.small_green_bus;
                    break;
            }
            return AndroidBitmapDescriptorFactory.FromResource(iconId);
        }
    }
}
