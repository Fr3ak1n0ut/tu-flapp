using System;
namespace Flapp1
{
    public class Flight
    {
        public string flightNumber{get; set;}
        public string airlineName{ get; set; }
        public string flightDate { get; set; }
        public string startTime{ get; set; }
        public string endTime{ get; set; }
        public int gateNumber{ get; set; }
        public int terminal{ get; set; }
        public string seatNumber{ get; set; }
        public string flightStatus{ get; set; }
        private static Random rand = new Random();

        public Flight()
        {
        }

        public Flight(string airlineNameArg, string flightDateArg, 
                      string startTimeArg, string endTimeArg){
            airlineName = airlineNameArg;
            flightDate = flightDateArg;
            startTime = startTimeArg;
            endTime = endTimeArg;
            flightNumber = generateRandomFlightNumber();
            terminal = rand.Next(0, 20);
            seatNumber = generateRandomSeatNumber();
            flightStatus = generateRandomFlightStatus();
            gateNumber = rand.Next(0, 10);

			

        }

        public string generateRandomFlightNumber(){
			string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randFlightNumber = "";
			int index = rand.Next(0, chars.Length - 2);
            randFlightNumber = chars[index].ToString() + chars[index + 1].ToString() + "-" + 
                rand.Next(100, 900).ToString();
            return randFlightNumber;
        }

		public string generateRandomSeatNumber()
		{
			string chars = "ABC";
			string randSeatNumber = "";
			int index = rand.Next(0, chars.Length - 1);
            randSeatNumber = rand.Next(1, 40).ToString() + chars[index];
            return randSeatNumber;
		}

		public string generateRandomFlightStatus()
		{
            string[] statuses = new String[]{"Ontime","Delayed","Cancelled"};
			string status = "Ontime";
			int index = rand.Next(0, statuses.Length - 1);
			status = statuses[index];
			return status;
		}
    }
}
