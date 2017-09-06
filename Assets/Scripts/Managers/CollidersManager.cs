using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CollidersManager : MonoBehaviour
    {
        //Horizontal Colliders
        public GameObject screenBottomCollider;
        public GameObject topCollider;

        private void Awake()
        {
            SetCollidersPosition();
            SetCollidersSize();
        }

        void SetCollidersSize()
        {
            var horizontalCollidersWidth = 4 * Camera.main.orthographicSize;

            //Set horizontal colliders scale
            Vector2 scaleSize = new Vector2(horizontalCollidersWidth, 1);

            topCollider.GetComponent<BoxCollider2D>().size = scaleSize;
            screenBottomCollider.GetComponent<BoxCollider2D>().size = scaleSize;
        }

        void SetCollidersPosition()
        { 

            Vector3 topMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
            Vector3 bottomMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));

            //If an enemy colliders with it, it goes to the enemy pool
            //Set bottom collider off screen. Once player colliders with it, it's game over man!
            topCollider.transform.position = new Vector3(topMiddle.x, topMiddle.y + 3);

            screenBottomCollider.transform.position = bottomMiddle;
        }
    }
}
