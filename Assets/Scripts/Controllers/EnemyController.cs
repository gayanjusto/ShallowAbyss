using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EnemyController : MonoBehaviour
    {

        public float horizontalDirection;
        public float verticalDirection;
        public float minSpeed;
        public float maxSpeed;

        SpriteRenderer spriteRenderer;

        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetRandomSpeed();

            if (minSpeed == 0)
            {
                minSpeed = maxSpeed;
            }

            //Set random horizontal direction
            horizontalDirection *= RandomValueTool.GetRandomValue(0, 1) * 2 - 1;
        }

        private void Update()
        {
            MovementService.TranslateObjectVertically(this.gameObject, verticalDirection, minSpeed);
            MovementService.TranslateObjectHorizontally(this.gameObject, horizontalDirection, minSpeed);

            SetSpriteDirection();
        }

        public void SetRandomSpeed()
        {
            minSpeed = RandomValueTool.GetRandomFloatValue(minSpeed, maxSpeed);
        }

        void SetSpriteDirection()
        {
            //moving to the left
            if(horizontalDirection == -1)
            {
                spriteRenderer.flipX = true;
            }else
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
