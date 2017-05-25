using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

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

        static string[] COUNTRIES = new string[] {
  "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
  "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina",
  "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
  "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
  "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
  "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory",
  "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
  "Cote d'Ivoire", "Cambodia", "Cameroon", "Canada", "Cape Verde",
  "Cayman Islands", "Central African Republic", "Chad", "Chile", "China",
  "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo",
  "Cook Islands", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic",
  "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic",
  "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea",
  "Estonia", "Ethiopia", "Faeroe Islands", "Falkland Islands", "Fiji", "Finland",
  "Former Yugoslav Republic of Macedonia", "France", "French Guiana", "French Polynesia",
  "French Southern Territories", "Gabon", "Georgia", "Germany", "Ghana", "Gibraltar",
  "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau",
  "Guyana", "Haiti", "Heard Island and McDonald Islands", "Honduras", "Hong Kong", "Hungary",
  "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica",
  "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos",
  "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
  "Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands",
  "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
  "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia",
  "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand",
  "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "North Korea", "Northern Marianas",
  "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
  "Philippines", "Pitcairn Islands", "Poland", "Portugal", "Puerto Rico", "Qatar",
  "Reunion", "Romania", "Russia", "Rwanda", "Sqo Tome and Principe", "Saint Helena",
  "Saint Kitts and Nevis", "Saint Lucia", "Saint Pierre and Miquelon",
  "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Saudi Arabia", "Senegal",
  "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
  "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Korea",
  "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden",
  "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "The Bahamas",
  "The Gambia", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey",
  "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Virgin Islands", "Uganda",
  "Ukraine", "United Arab Emirates", "United Kingdom",
  "United States", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan",
  "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Wallis and Futuna", "Western Sahara",
  "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"
};

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            //locationText = FindViewById<TextView>(Resource.Id.location_text);
            timeText = FindViewById<TextView>(Resource.Id.time_text);
            dateText = FindViewById<TextView>(Resource.Id.date_text);

            dateButton = FindViewById<Button>(Resource.Id.date_button);
            timeButton = FindViewById<Button>(Resource.Id.time_button);
            submitButton = FindViewById<Button>(Resource.Id.submit_button);

            origin_edit = FindViewById<AutoCompleteTextView>(Resource.Id.origin_edit);
            destination_edit = FindViewById<AutoCompleteTextView>(Resource.Id.destination_edit);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.list, COUNTRIES);
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
                var intent = new Intent(this, typeof(FlightInfoActivity));
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