using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Syncfusion.SfCalendar.XForms;
using System.Globalization;
using System.ComponentModel;

namespace Calendar
{
	public partial class MainPage : ContentPage
	{
        List<EventInfo> results;
        public MainPage()
		{
			InitializeComponent();

            results = App.eventDB.GetDataFromDB("");

            calendar.Locale = new System.Globalization.CultureInfo("vi-VN");

            //calendar.ShowInlineEvents = true;
            calendar.OnMonthCellLoaded += Handle_OnMonthCellLoaded;            
        }

        void Handle_OnMonthCellLoaded(object sender, MonthCell args)
        {
            VacaInfo vacaInfo = new VacaInfo(args.Date);
            foreach (var events in results)
            {
                if (convertStringToDateTime(events.StartTime).ToString("yyyy-MM-dd") 
                    == args.Date.ToString("yyyy-MM-dd"))
                    vacaInfo.backGroundColor = events.Color;
            }
            args.CellBindingContext = vacaInfo;
            if(args.Date.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                vacaInfo.textColor = "Red";
            }
        }
        DateTime convertStringToDateTime(string data)
        {
            int[] results = new int[6];
            results[0] = int.Parse(data.Substring(0, 4));
            results[1] = int.Parse(data.Substring(4, 2));
            results[2] = int.Parse(data.Substring(6, 2));
            results[3] = int.Parse(data.Substring(8, 2));
            results[4] = int.Parse(data.Substring(10, 2));
            results[5] = int.Parse(data.Substring(12, 2));
            return new DateTime(results[0], results[1], results[2],
                results[3], results[4], results[5]);
        }
    }

    public class VacaInfo
    {
        public DateTime Date { get; set; }        
        public int lunarDay;
        public int lunarMonth { get; set; }
        public int lunarYear { get; set; }
        public string lunarDayStr { get; set; }
        public string lunarMonthStr { get; set; }
        public string lunarYearStr { get; set; }
        public string backGroundColor { get; set; }
        public string textColor { get; set; }
        public string lunarDayBinding { get; set; }

        public VacaInfo() { }
        public VacaInfo(DateTime date)
        {
            Date = date;
            backGroundColor = "Transparent";
            textColor = "Black";
            VietnameseCalendar vCal = new VietnameseCalendar();
            lunarDay = vCal.GetDayOfMonth(date);
            lunarMonth = vCal.GetMonth(date);
            lunarYear = vCal.GetYear(date);
            lunarDayStr = VietnameseCalendar.GetDayName(date);
            lunarMonthStr = VietnameseCalendar.GetMonthName(lunarYear, lunarMonth);
            lunarYearStr = VietnameseCalendar.GetYearName(lunarYear);
            lunarDayBinding = lunarDay.ToString();
            if (lunarDay == 1) lunarDayBinding += "/" + lunarMonth.ToString();            
        }
    }
}
