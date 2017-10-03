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

        protected SpriteRenderer spriteRenderer;

        protected void Start()
        {
            GetSpriteRenderer();
            SetRandomSpeed();

            if (minSpeed == 0)
            {
                minSpeed = maxSpeed;
            }

            //Set random horizontal direction
            horizontalDirection *= RandomValueTool.GetRandomValue(0, 1) * 2 - 1;
        }

        protected virtual void GetSpriteRenderer()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        protected void Update()
        {
            MovementService.TranslateObjectVertically(this.gameObject, verticalDirection, minSpeed);
            MovementService.TranslateObjectHorizontally(this.gameObject, horizontalDirection, minSpeed);

            SetSpriteDirection();
        }

        public void SetRandomSpeed()
        {
            minSpeed = RandomValueTool.GetRandomFloatValue(minSpeed, maxSpeed);
        }

        protected void SetSpriteDirection()
        {
            //moving to the left
            if(horizontalDirection == -1)
            {
                spriteRenderer.flipX = true;
            }else if(horizontalDirection == 1)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
