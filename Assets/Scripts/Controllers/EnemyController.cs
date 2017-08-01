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
        }

        private void Update()
        {
            MovementService.TranslateObjectVertically(this.gameObject, 1, minSpeed);
            MovementService.TranslateObjectHorizontally(this.gameObject, horizontalDirection, minSpeed);
        }
    }
}
