using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace Flapp1
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private TextView timeText;
        private TextView locationText;
        private TextView dateText;
        private Button dateButton;
        private Button timeButton;

        private int hour;
        private int minute;

        const int TIME_DIALOG_ID = 0;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            locationText = FindViewById<TextView>(Resource.Id.location_text);
            timeText = FindViewById<TextView>(Resource.Id.time_text);
            dateText = FindViewById<TextView>(Resource.Id.date_text);
            dateButton = FindViewById<Button>(Resource.Id.date_button);
            timeButton = FindViewById<Button>(Resource.Id.time_button);
            dateButton.Click += DateSelect_OnClick;
            timeButton.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);

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