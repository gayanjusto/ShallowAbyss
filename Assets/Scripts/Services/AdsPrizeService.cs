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


            int rndVal = RandomValueTool.GetRandomValue(0, prizeChildren.Length - 1);


            return prizeChildren[rndVal];
        }
    }
}
