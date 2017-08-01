using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class ScreenPositionService
    {
        public static Vector3 GetRightEdge(Camera mainCamera)
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        }
        public static Vector3 GetLeftEdge(Camera mainCamera)
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        }
        public static Vector3 GetTopEdge(Camera mainCamera)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        }
        public static Vector3 GetBottomEdge(Camera mainCamera)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        }
        public static Vector3 GetTopMiddleEdge(Camera mainCamera)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        }
        public static Vector3 GetBottomMiddleEdge(Camera mainCamera)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
        }
    }
}
