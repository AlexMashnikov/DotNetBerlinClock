using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock {
    public class TimeConverter : ITimeConverter {
        const string YellowColor = "Y";
        const string RedColor = "R";
        const string Off = "O";
        const int HoursIndex = 0;
        const int MinutesIndex = 1;
        const int SecondsIndex = 2;

        public string convertTime(string aTime) {
            var splittedTime = aTime.Split(':').Select(Int32.Parse).ToList();
            if(!splittedTime.Any() && splittedTime.Count != 3) {
                throw new FormatException("Invalid time format");
            }
            return GetSeconds(splittedTime[SecondsIndex]) + Environment.NewLine + 
                GetTopRowHours(splittedTime[HoursIndex]) + Environment.NewLine +
                GetBottomRowHours(splittedTime[HoursIndex]) + Environment.NewLine +
                GetTopRowMinutes(splittedTime[MinutesIndex]) + Environment.NewLine +
                GetBottomRowMinutes(splittedTime[MinutesIndex]);
        }

        string GetSeconds(int value) {
            if(value % 2 == 0)
                return YellowColor;
            else
                return Off;
        }
        string GetTopRowHours(int value) {
            return GetOnOff(4, GetTopNumberOfOnSigns(value));
        }
        string GetOnOff(int lamps, int onSigns) {
            return GetOnOff(lamps, onSigns, RedColor);
        }
        string GetOnOff(int lamps, int onSigns, string onSign) {
            string result = string.Empty;
            for(int i = 0; i < onSigns; i++) {
                    result += onSign;
            }
            for(int i = 0; i < (lamps - onSigns); i++) {
                    result += Off;
            }
            return result;
        }
        int GetTopNumberOfOnSigns(int value) {
            return (value - (value % 5)) / 5;
        }
        string GetBottomRowHours(int number) {
            return GetOnOff(4, number % 5);
        }
        string GetTopRowMinutes(int number) {
            return GetOnOff(11, GetTopNumberOfOnSigns(number), YellowColor).Replace("YYY", "YYR");
        }
        string GetBottomRowMinutes(int number) {
            return GetOnOff(4, number % 5, YellowColor);
        }
    }
}