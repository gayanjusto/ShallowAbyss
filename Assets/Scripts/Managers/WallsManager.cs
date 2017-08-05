using UnityEngine;

namespace Assets.Scripts.Managers
{
    public enum WallSide
    {
        Left = 1,
        Right = 2
    }
    public class WallsManager : MonoBehaviour
    {
        public float wallVerticalLimitToSpawnAnother;
        public float wallVerticalLimitToDisable;
        public float wallVerticalSpawnPosition;
        public float wallInitialVerticalSpawnPosition;


        public Transform currentLeftWall;
        public Transform currentRightWall;

        public Transform perviousLeftWall;
        public Transform previousRightWall;

        public GameObject leftWallsPool;
        public GameObject rightWallsPool;


        private void Start()
        {
            InstantiateInitialWalls();
            SetWallsPositionToScreen();
        }
        private void Update()
        {
            // We use local position since wall is child of WallsPool
            if (currentRightWall.transform.localPosition.y > wallVerticalLimitToSpawnAnother)
            {
                previousRightWall = currentRightWall;
                currentRightWall = GetNewWall(WallSide.Right, wallVerticalSpawnPosition);
            }

            // Check if previous wall is off screen
            if (previousRightWall != null)
            {
                if (previousRightWall.transform.localPosition.y > wallVerticalLimitToDisable)
                    previousRightWall.gameObject.SetActive(false);
            }

            // We use local position since wall is child of WallsPool
            if (currentLeftWall.transform.localPosition.y > wallVerticalLimitToSpawnAnother)
            {
                perviousLeftWall = currentLeftWall;
                currentLeftWall = GetNewWall(WallSide.Left, wallVerticalSpawnPosition);
            }

            // Check if previous wall is off screen
            if (perviousLeftWall != null)
            {
                if (perviousLeftWall.transform.localPosition.y > wallVerticalLimitToDisable)
                    perviousLeftWall.gameObject.SetActive(false);
            }
        }

        void InstantiateInitialWalls()
        {
            currentLeftWall = GetNewWall(WallSide.Left, wallInitialVerticalSpawnPosition);
            currentRightWall = GetNewWall(WallSide.Right, wallInitialVerticalSpawnPosition);
        }

        void SetWallsPositionToScreen()
        {
            float leftEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
            float rightEdge = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

            leftWallsPool.transform.position = new Vector3(leftEdge -.5f, 0, 0);
            rightWallsPool.transform.position = new Vector3(rightEdge, 0, 0);
        }

        Transform GetNewWall(WallSide wallSide, float verticalPosition)
        {
            Transform _wall = null;
            GameObject _wallPoolToIterate = wallSide == WallSide.Left ? leftWallsPool : rightWallsPool;

            //if its the first walls to be instantiated, they must start on screen
            foreach (Transform wall in _wallPoolToIterate.transform)
            {
                if (!wall.gameObject.active)
                {
                    wall.gameObject.SetActive(true);

                    
                    wall.transform.localPosition = new Vector3(wall.transform.localPosition.x, verticalPosition, -10);
                    _wall = wall;
                    break;
                }
            }

            return _wall;
        }

        public Vector3 GetLeftWallScale()
        {
            return currentLeftWall.transform.localScale;
        }

        public Vector3 GetRighttWallScale()
        {
            return currentLeftWall.transform.localScale;
        }
    }
}
