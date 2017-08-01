using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EnemyController : MonoBehaviour
    {
        Rigidbody2D enemyRigidBody;

        public float horizontalDirection;
        public float minSpeed;
        public float maxSpeed;

        private void Start()
        {
            enemyRigidBody = GetComponent<Rigidbody2D>();
            minSpeed = RandomValueTool.GetRandomFloatValue(minSpeed, maxSpeed);

            if(minSpeed == 0)
            {
                minSpeed = maxSpeed;
            }

            //Set random horizontal direction
            horizontalDirection *= RandomValueTool.GetRandomValue(0, 1) * 2 - 1;
        }

        private void Update()
        {
            MovementService.TranslateObjectVertically(this.gameObject, 1, minSpeed);
            MovementService.TranslateObjectHorizontally(this.gameObject, horizontalDirection, minSpeed);
        }
    }
}
