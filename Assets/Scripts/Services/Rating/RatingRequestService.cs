using Assets.Scripts.DAO;
using Assets.Scripts.Entities.Rating;
using System;
using UnityEngine;

namespace Assets.Scripts.Services.Rating
{
    public class RatingRequestService
    {
        static bool? _ratingRequestDisabled;
        public static bool ratingRequestDisabled
        {
            get
            {
                if (_ratingRequestDisabled == null)
                {
                    var ratingRequestService = new RatingRequestService();
                    var request = ratingRequestService.GetLastRequestData();
                    bool canRequest = !request.UserHasPostponedOrAccepted();
                    _ratingRequestDisabled = canRequest;
                }

                return _ratingRequestDisabled.Value;
            }
        }
        string ratingRequestDataFilePath
        {
            get
            {
                return Application.persistentDataPath + "/ratingRequest.dat";
            }
        }

        public RatingRequest GetLastRequestData()
        {
            var appReader = new ApplicationDataReader<RatingRequest>();
            var requestData = appReader.LoadData(ratingRequestDataFilePath);
            if (requestData == null)
                requestData = new RatingRequest();

            return requestData;
        }

        public bool CanShowRequestRating()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return false;

            var lastRequestData = GetLastRequestData();

            if (lastRequestData.UserHasPostponedOrAccepted())
                return false;

            if (!lastRequestData.userHasClickedToRate &&
                lastRequestData.UserHasPlayedEnoughTimesToShowRequest() &&
                !lastRequestData.UserHasPostponedEnough() &&
                lastRequestData.HasPassedEnoughDaysToRequestAgain())
            {
                return true;
            }


            return false;
        }

        public void SaveNewRequest(bool userHasClickedToRate)
        {
            var request = GetLastRequestData();

            if (!userHasClickedToRate)
                request.timesUserHastPostponedRequest++;

            request.userHasClickedToRate = userHasClickedToRate;
            request.lastDayRequest = DateTime.Today.Day;
            request.lastMonthRequest = DateTime.Today.Month;
            request.lastYearRequest = DateTime.Today.Year;

            var appReader = new ApplicationDataReader<RatingRequest>();
            appReader.SaveDataAsync(request, ratingRequestDataFilePath);
        }

        public void IncreaseTimesUserHasPlayed()
        {
            var ratingRequestService = new RatingRequestService();
            var request = GetLastRequestData();


            request.timesUserHasPlayed++;

            var appReader = new ApplicationDataReader<RatingRequest>();
            appReader.SaveDataAsync(request, ratingRequestDataFilePath);
        }
    }
}
