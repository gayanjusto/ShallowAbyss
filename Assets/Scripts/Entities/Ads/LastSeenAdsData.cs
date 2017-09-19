using System;

namespace Assets.Scripts.Entities.Ads
{
    [Serializable]
    public class LastSeenAdsData
    {
        public int day;
        public int month;
        public int year;


        public bool CanShowAd()
        {
            var lastSeenDate = new DateTime(year, month, day);
            return lastSeenDate.AddDays(1) <= DateTime.Today;
        }

        public void SetNewDate(DateTime date)
        {
            this.day = date.Day;
            this.month = date.Month;
            this.year = date.Year;
        }
    }
}
