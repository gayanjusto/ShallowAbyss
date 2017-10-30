using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Dictionaries
{
    public class EnemyKindLevel
    {
        static Dictionary<int, int> _standardLevel;
        static Dictionary<int, int> _lightLevel;
        static Dictionary<int, int> _chargerLevel;
        static Dictionary<int, int> _heavyLevel;
        static Dictionary<int, int> _trapperLevel;



        public static Dictionary<int, int> GetStandardKindLevel()
        {
            if (_standardLevel == null)
            {
                _standardLevel = new Dictionary<int, int>();
                int baseVal = EnemyTypeLevel.GetAllEnemiesLevels()[EnemyTypeEnum.Standard];
                int nextVal = baseVal + 3;
                _standardLevel.Add(0, baseVal);
                _standardLevel.Add(1, nextVal);
                _standardLevel.Add(2, nextVal * 2);
                _standardLevel.Add(3, nextVal * 4 + 1);
            }

            return _standardLevel;
        }

        public static Dictionary<int, int> GetLightKindLevel()
        {
            if (_lightLevel == null)
            {
                _lightLevel = new Dictionary<int, int>();
                int baseVal = EnemyTypeLevel.GetAllEnemiesLevels()[EnemyTypeEnum.Light];
                _lightLevel.Add(0, baseVal);
                _lightLevel.Add(1, baseVal * 2 + 3);
            }

            return _lightLevel;
        }

        public static Dictionary<int, int> GetChargerKindLevel()
        {
            if (_chargerLevel == null)
            {
                _chargerLevel = new Dictionary<int, int>();
                int baseVal = EnemyTypeLevel.GetAllEnemiesLevels()[EnemyTypeEnum.Charger];
                _chargerLevel.Add(0, baseVal);
                _chargerLevel.Add(1, baseVal * 2);

            }

            return _chargerLevel;
        }

        public static Dictionary<int, int> GetHeavyKindLevel()
        {
            if (_heavyLevel == null)
            {
                _heavyLevel = new Dictionary<int, int>();
                int baseVal = EnemyTypeLevel.GetAllEnemiesLevels()[EnemyTypeEnum.Heavy];
                _heavyLevel.Add(0, baseVal);
                _heavyLevel.Add(1, baseVal + 10);
            }

            return _heavyLevel;
        }

        public static Dictionary<int, int> GetTrapperKindsLevel()
        {
            if (_trapperLevel == null)
            {
                _trapperLevel = new Dictionary<int, int>();
                int baseVal = EnemyTypeLevel.GetAllEnemiesLevels()[EnemyTypeEnum.Trapper];
                _trapperLevel.Add(0, baseVal);
            }

            return _trapperLevel;
        }
    }
}
