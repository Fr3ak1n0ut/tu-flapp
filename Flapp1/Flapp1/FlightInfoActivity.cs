using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Flapp1
{
    [Activity(Label = "FlightInfoActivity")]
    public class FlightInfoActivity : Activity
    {
        private TextView infoText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FlightInfo);
            var flightInfo = Intent.Extras.GetStringArrayList("flight_info") ?? new string[0];
            infoText = FindViewById<TextView>(Resource.Id.info_text);
            infoText.Text = flightInfo[0]+" "+flightInfo[1]+" "+flightInfo[2]+ " " + flightInfo[3];
            // Create your application here
        }
    }
}