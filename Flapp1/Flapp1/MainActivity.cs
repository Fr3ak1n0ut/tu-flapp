using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.IO;
using System.Collections.Generic;

namespace Flapp1
{
    [Activity(Label = "@string/FlightSearch", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private AutoCompleteTextView origin_edit;
        private AutoCompleteTextView destination_edit;
        private TextView timeText;
        private TextView locationText;
        private TextView dateText;
        private Button dateButton;
        private Button timeButton;
        private Button submitButton;

        private int hour;
        private int minute;

        const int TIME_DIALOG_ID = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Loading cities from assets
            var CITIES = new List<string>();
			AssetManager assets = this.Assets;
			using (StreamReader sr = new StreamReader(assets.Open("ListOfCities.txt")))
			{
				string city = "";
				while ((city = sr.ReadLine()) != null)
				{
                    CITIES.Add(city);
				}
			}

            //locationText = FindViewById<TextView>(Resource.Id.location_text);
            timeText = FindViewById<TextView>(Resource.Id.time_text);
            dateText = FindViewById<TextView>(Resource.Id.date_text);

            dateButton = FindViewById<Button>(Resource.Id.date_button);
            timeButton = FindViewById<Button>(Resource.Id.time_button);
            submitButton = FindViewById<Button>(Resource.Id.submit_button);

            origin_edit = FindViewById<AutoCompleteTextView>(Resource.Id.origin_edit);
            destination_edit = FindViewById<AutoCompleteTextView>(Resource.Id.destination_edit);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list, CITIES);
            origin_edit.Adapter = adapter;
            destination_edit.Adapter = adapter;


            DateTime timeNow = DateTime.Now;
            dateText.Text = timeNow.DayOfWeek + ", " + timeNow.ToString("MMMM") + " " + timeNow.Day + ", " + timeNow.Year;
            timeText.Text = timeNow.ToString("HH:mm");
            dateButton.Click += DateSelect_OnClick;
            timeButton.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);
            submitButton.Click += Submit_OnClick;

            // Get the current time
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            // Display the current date
            UpdateDisplay();

            void DateSelect_OnClick(object sender, EventArgs eventArgs)
            {
                DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (DateTime time)
                {
                    dateText.Text = time.ToLongDateString();
                });
                frag.Show(FragmentManager, DatePickerFragment.TAG);
            }

            void Submit_OnClick(object sender, EventArgs eventArgs)
            {
                var intent = new Intent(this, typeof(FlightListActivity));
                string[] flightInfo = new string[4];
                flightInfo[0] = origin_edit.Text;
                flightInfo[1] = destination_edit.Text;
                flightInfo[2] = timeText.Text;
                flightInfo[3] = dateText.Text;
                intent.PutStringArrayListExtra("flight_info", flightInfo);
                StartActivity(intent);
            }

            // Updates the time we display in the TextView

        }
        private void UpdateDisplay()
        {
            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            timeText.Text = time;
        }

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;
            UpdateDisplay();
        }
        protected override Dialog OnCreateDialog(int id)
        {
            if (id == TIME_DIALOG_ID)
                return new TimePickerDialog(this, TimePickerCallback, hour, minute, false);

            return null;
        }
    }
}