using System;

namespace Assets.Scripts.Entities.Rating
{
    [Serializable]
    public class RatingRequest
    {
        public int lastDayRequest;
        public int lastMonthRequest;
        public int lastYearRequest;
        public bool userHasClickedToRate;
        public int timesUserHastPostponedRequest;
        public int timesUserHasPlayed;
        
        DateTime? GetLastRequestDate()
        {
            if (lastDayRequest == 0 && lastMonthRequest == 0 && lastYearRequest == 0)
                return null;

            return new DateTime(lastYearRequest, lastMonthRequest, lastDayRequest);
        }

        public bool UserHasPostponedEnough()
        {
            return timesUserHastPostponedRequest >= 2;
        }

        public bool UserHasPlayedEnoughTimesToShowRequest()
        {
            return timesUserHasPlayed >= 5;
        }
        public bool HasPassedEnoughDaysToRequestAgain()
        {
            if (GetLastRequestDate() == null)
                return true;

            var diff = (DateTime.Today - GetLastRequestDate().Value).TotalDays;

            return diff > 1;
        }

        public bool UserHasPostponedOrAccepted()
        {
            return UserHasPostponedEnough() || this.userHasClickedToRate;
        }

        public bool RequestToReconsiderReview()
        {
            if ((DateTime.Today - GetLastRequestDate().Value).TotalDays >= 365)
                return true;

            return false;
        }
    }
}
