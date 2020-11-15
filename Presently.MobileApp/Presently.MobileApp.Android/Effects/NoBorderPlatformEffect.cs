using Android.Graphics.Drawables;
using Android.Widget;
using Presently.MobileApp.Droid.Effects;
using Presently.MobileApp.Effects;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.ViewGroup;

[assembly: ExportEffect(typeof(NoBorderPlatformEffect), nameof(NoBorderEffect))]
namespace Presently.MobileApp.Droid.Effects
{
    public class NoBorderPlatformEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Control is EditText || Control is PickerEditText)
                {
                    Control.Background = new ColorDrawable(Android.Graphics.Color.Transparent);
                    Control.SetPadding(25, 0, 25, 0);
                }
                else if (Control is Android.Widget.DatePicker || Control is Android.Widget.Spinner)
                {
                    Control.Background = null;
                    var datePicker = Control as Android.Widget.DatePicker;
                    var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                    layoutParams.SetMargins(0, 0, 0, 0);
                    datePicker.LayoutParameters = layoutParams;
                    Control.LayoutParameters = layoutParams;
                    datePicker.SetPadding(25, 0, 25, 0);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }
}