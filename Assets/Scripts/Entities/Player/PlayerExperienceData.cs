using System;
using System.Linq;

namespace Assets.Scripts.Entities.Player
{
    [Serializable]
    public class PlayerExperienceData
    {
        //game duration in seconds
        float?[] gameDuration;
        int lastInsertedIndex;

        public void InsertGameDuration(float gameDurationTime)
        {
            if (gameDuration == null)
                gameDuration = new float?[3];

            bool hasInserted = false;
            for (int i = 0; i < gameDuration.Length; i++)
            {
                if (gameDuration[i] == null)
                {
                    gameDuration[i] = gameDurationTime;
                    lastInsertedIndex = i;
                    hasInserted = true;
                    break;
                }

                //If it's the last iteration, overwrite the first one
                if (i == gameDuration.Length - 1)
                {
                    gameDuration[0] = gameDurationTime;
                    break;
                }
            }

            //override the next inserted index
            if (!hasInserted)
            {
                //if it was the last index, override the first
                if(lastInsertedIndex+1 >= gameDuration.Length)
                {
                    lastInsertedIndex = 0;
                    gameDuration[lastInsertedIndex] = gameDurationTime;
                    return;
                }

                lastInsertedIndex++;
                gameDuration[lastInsertedIndex] = gameDurationTime;
            }
        }

        public float GetAverageGameTime()
        {
            return gameDuration.Where(x => x.HasValue).Sum(x => x.Value) / gameDuration.Where(x => x.HasValue).Count();
        }

        public float GetBestTime()
        {
            var withValues = gameDuration.Where(x => x.HasValue);
            if (withValues == null || withValues.Count() == 0)
                return 0;
            return withValues.Max().Value;
        }
    }
}
