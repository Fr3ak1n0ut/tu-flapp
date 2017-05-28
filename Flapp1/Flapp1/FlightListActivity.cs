
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
		static string[] FLIGHTS = new string[] {"Emirates",
			"Qatar Airways", "Singapore Airlines","Cathay Pacific",
			"ANAs","Etihad Airways","Turkish Airlines","EVA Air",
			"Qantas Airways","Lufthansa","Garuda Indonesia","Hainan Airlines",
			"Thai Airways","Air France","Swiss Int’l Air Lines",
			"Asiana Airlines","Air New Zealand","Virgin Australia","Austrian"};

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


			availableFlights = FindViewById<ListView>(Resource.Id.airlineList);
			ArrayAdapter<string> airlineListAdapter = new ArrayAdapter<string>
				(this, Android.Resource.Layout.SimpleListItem1, FLIGHTS);
			availableFlights.Adapter = airlineListAdapter;
			availableFlights.ItemClick += (sender, e) =>
			{
				Flight selectedFlight = new Flight(FLIGHTS[e.Position], flightInfo[3], flightInfo[2], "");
				Intent intent = new Intent(this, typeof(FlightInfoActivity));
				intent.PutExtra("flight_obj", JsonConvert.SerializeObject(selectedFlight));
				this.StartActivity(intent);
			};
		}
	}
}
