using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CollidersManager : MonoBehaviour
    {
        //Horizontal Colliders
        public GameObject topCollider;
        public GameObject bottomCollider;

        private void Awake()
        {
            SetCollidersPosition();
            SetCollidersSize();
        }

        void SetCollidersSize()
        {
            var horizontalCollidersWidth = 4 * Camera.main.orthographicSize;
            var verticalCollidersWidth = horizontalCollidersWidth * Camera.main.aspect;

            //Set horizontal colliders scale
            topCollider.GetComponent<BoxCollider2D>().size = new Vector2(horizontalCollidersWidth, 1);
            bottomCollider.GetComponent<BoxCollider2D>().size = new Vector2(horizontalCollidersWidth, 1);
        }

        void SetCollidersPosition()
        { 

            Vector3 topMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
            Vector3 bottomMiddle = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));

            //Set top collider off screen. Once player colliders with it, it's game over man!
            //If an enemy colliders with it, it goes to the enemy pool
            topCollider.transform.position = new Vector3(topMiddle.x, topMiddle.y + 3);

            bottomCollider.transform.position = bottomMiddle;
        }
    }
}
