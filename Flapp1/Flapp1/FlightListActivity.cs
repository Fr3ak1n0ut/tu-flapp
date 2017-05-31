
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
    [Activity(Label = "FlightListActivity")]
    public class FlightListActivity : Activity
	{
		private TextView infoText;
		private ListView availableFlights;
        //List of 32 Airlines
		static string[] ALLFLIGHTS = new string[] {"Emirates",
			"Qatar Airways", "Singapore Airlines","Cathay Pacific",
            "Etihad Airways","Turkish Airlines","EVA Air","Qantas Airways",
            "Lufthansa","Garuda Indonesia","Hainan Airlines","Thai Airways",
            "Pakistan International Airlines","Air France",
            "Swiss Int’l Air Lines","Asiana Airlines","Air New Zealand",
            "Virgin Australia","Austrian","Bangkok Airways","Japan Airlines",
            "Air India","British Airways","Air Canada","Malaysia Airlines",
            "Korean Air","Oman Air","SriLankan Airlines","Air Berlin",
            "Gulf Air","Saudi Arabian Airlines","China Airlines"};

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.FlightList);
			var flightInfo = Intent.Extras.GetStringArrayList("flight_info")
								   ?? new string[0];
			infoText = FindViewById<TextView>(Resource.Id.info_text);
			infoText.Text = "Flights from " + flightInfo[0] + " to "
				+ flightInfo[1] + " on " + flightInfo[3]
				+ " at " + flightInfo[2];
            Random rand = new Random();
            int randomIndex = rand.Next(0, 27);
            var flights = ALLFLIGHTS.Skip(randomIndex).Take(5).ToArray<string>();
			availableFlights = FindViewById<ListView>(Resource.Id.airlineList);
			ArrayAdapter<string> airlineListAdapter = new ArrayAdapter<string>
				(this, Android.Resource.Layout.SimpleListItem1, flights);
			availableFlights.Adapter = airlineListAdapter;
			availableFlights.ItemClick += (sender, e) =>
			{
				Flight selectedFlight = new Flight(flights[e.Position], flightInfo[3], flightInfo[2], "");
				Intent intent = new Intent(this, typeof(FlightInfoActivity));
				intent.PutExtra("flight_obj", JsonConvert.SerializeObject(selectedFlight));
				this.StartActivity(intent);
			};
		}
	}
}
