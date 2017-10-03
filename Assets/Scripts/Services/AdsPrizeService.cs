using Assets.Scripts.Entities.Ads;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class AdsPrizeService
    {
        public static AdsPrize GetRandomPrize(int currentScore)
        {
            var prizeChildren = ClassChildren.GetArrayOfType<AdsPrize>(null);

            //if the player's score is less than 30 always give him credits multiplier
            //other prizes can be generated only with a better score
            if (currentScore < 30)
            {
                return prizeChildren[0];
            }

            int rndVal = RandomValueTool.GetRandomValue(0, prizeChildren.Length - 1);


            return prizeChildren[rndVal];
        }
    }
}
