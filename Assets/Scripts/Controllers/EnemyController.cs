using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        Rigidbody2D enemyRigidBody;

        public float horizontalDirection;
        public float verticalDirection;
        public float minSpeed;
        public float maxSpeed;

        private void Start()
        {
            enemyRigidBody = GetComponent<Rigidbody2D>();
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
        }

        public void SetRandomSpeed()
        {
            minSpeed = RandomValueTool.GetRandomFloatValue(minSpeed, maxSpeed);
        }
    }
}
