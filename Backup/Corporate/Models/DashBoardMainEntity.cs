using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Corporate.Models
{
    public class DashBoard
    {
        public DashBoardMainEntity PayFin { get; set; }
        public DashBoardOrderEntity Orders { get; set; }
    }

    public class DashBoardMainEntity
    {
        public List<ThreeDayPayment> ThreeDays { get; set; }
        public List<MonthPayments> FinYear { get; set; }
    }

    public class DashBoardOrderEntity
    {
        public List<SevenDayPO> SevenDaysPO { get; set; }
        public List<SevenDaySO> SevenDaysSO { get; set; }
    }

    public class ThreeDayPayment
    {
        public string Date { get; set; }
        public decimal ThreeDaysPayments { get; set; }
    }
   
    public class MonthPayments
    {
        public string CurrentYear { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string August { get; set; }
        public string Sept { get; set; }
        public string Oct { get; set; }
        public string Nov { get; set; }
        public string Decm { get; set; }
        public string Jan { get; set; }
        public string Feb { get; set; }
        public string March { get; set; }
        public string Total { get; set; }

    }

public class SevenDaySO
{
    public string Date { get; set; }
    public int SevenDaysSO { get; set; }
}
public class SevenDayPO
{
    public string Date { get; set; }
    public int SevenDaysPO { get; set; }
}
}
