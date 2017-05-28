using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Flapp1
{
    [Activity(Label = "@string/FlightInfo")]
    public class FlightInfoActivity : Activity
    {
        private TextView airlineText;
        private TextView flightText;
        private TextView departureText;
        private TextView departureAtText;
        private TextView terminalText;
        private TextView gateText;
        private TextView statusText;
        private TextView seatText;
        private Flight flight;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.FlightInfo);

            flight = JsonConvert.DeserializeObject<Flight>(Intent.GetStringExtra("flight_obj"));

            airlineText = FindViewById<TextView>(Resource.Id.airlineText);
            flightText = FindViewById<TextView>(Resource.Id.flightText);
            departureText = FindViewById<TextView>(Resource.Id.departureText);
            departureAtText = FindViewById<TextView>(Resource.Id.departureAtText);
            terminalText = FindViewById<TextView>(Resource.Id.terminalText);
            gateText = FindViewById<TextView>(Resource.Id.gateText);
            statusText = FindViewById<TextView>(Resource.Id.statusText);
            seatText = FindViewById<TextView>(Resource.Id.seatText);

            airlineText.Text += flight.airlineName;
            flightText.Text += flight.flightNumber;
            departureText.Text += flight.flightDate;
            departureAtText.Text += flight.startTime;
            terminalText.Text += flight.terminal;
            gateText.Text += flight.gateNumber;
            statusText.Text += flight.flightStatus;
            seatText.Text += flight.seatNumber;
        }
    }
}