using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class ScreenSizeService
    {
        //calculate physical inches with pythagoras theorem
        public static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));


            return diagonalInches;
        }

        public static bool DeviceIsTablet()
        {
            return DeviceDiagonalSizeInInches() > 6.5f;
        }
    }
}
