using Assets.Scripts.Enums;
using System.Collections.Generic;

namespace Assets.Scripts.Dictionaries
{
    public struct PropLevel
    {
        public string minLevel;
        public string maxLevel;
    }
    public class BackgroundPropsLevel
    {
        public static Dictionary<BackgroundContextEnum, PropLevel> _backgroundPropLevelDic;

        public static IDictionary<BackgroundContextEnum, PropLevel> GetBackgroundPropLevel()
        {
            if (_backgroundPropLevelDic == null)
            {
                FillPropsLevels();
            }

            return _backgroundPropLevelDic;
        }

        static void FillPropsLevels()
        {
            _backgroundPropLevelDic = new Dictionary<BackgroundContextEnum, PropLevel>();

            //Surface 1
            _backgroundPropLevelDic.Add(BackgroundContextEnum.Surface_1, new PropLevel() { minLevel = BackgroundContextEnum.Surface_1.ToString(), maxLevel = BackgroundContextEnum.Surface_2.ToString() });

            //Surface 2
            _backgroundPropLevelDic.Add(BackgroundContextEnum.Surface_2, new PropLevel() { minLevel = BackgroundContextEnum.Surface_1.ToString(), maxLevel = BackgroundContextEnum.Middle_1.ToString() });

            //Middle 1
            _backgroundPropLevelDic.Add(BackgroundContextEnum.Middle_1, new PropLevel() { minLevel = BackgroundContextEnum.Surface_2.ToString(), maxLevel = BackgroundContextEnum.Middle_1.ToString() });

            //Middle 2
            _backgroundPropLevelDic.Add(BackgroundContextEnum.Middle_2, new PropLevel() { minLevel = BackgroundContextEnum.Middle_1.ToString(), maxLevel = BackgroundContextEnum.Abyss_1.ToString() });

            //Abyss
            _backgroundPropLevelDic.Add(BackgroundContextEnum.Abyss_1, new PropLevel() { minLevel = BackgroundContextEnum.Middle_2.ToString(), maxLevel = BackgroundContextEnum.Abyss_1.ToString()  });

            //Surface 2
            _backgroundPropLevelDic.Add(BackgroundContextEnum.Abyss_2, new PropLevel() { minLevel = BackgroundContextEnum.Abyss_2.ToString(), maxLevel = BackgroundContextEnum.Abyss_2.ToString()  });
        }

    }
}
