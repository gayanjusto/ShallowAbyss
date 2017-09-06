using System;
using UnityEngine;

namespace Assets.Scripts.Tools
{
    public class RandomValueTool
    {
        public static System.Random random;
        public static System.Random Random
        {
            get
            {
                if(random == null)
                {
                    random = new System.Random();
                }

                return random;
            }
        }

        public static int GetRandomValue(int min, int max)
        {
            return Random.Next(min, max + 1);
        }

        public static float GetRandomFloatValue(float min, float max)
        {
            return UnityEngine.Random.Range(min, max);
        }
    }
}
