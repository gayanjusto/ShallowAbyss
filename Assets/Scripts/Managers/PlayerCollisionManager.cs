using Assets.Scripts.Constants;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerCollisionManager : MonoBehaviour
    {
        public PlayerShieldManager playerShieldManager;
        public PlayerLifeManager playerLifeManager;
        public PlayerMovementInputController playerMovementInputController;
        public float timeToWaitAfterTopColliderHit;

        public bool hasHitTopCollider;
        public float freezeTime;

        public bool CanBeHit()
        {
            return playerLifeManager.CanBeHit() && playerShieldManager.CanBeHit();
        }

        private void Update()
        {
            if (hasHitTopCollider)
            {
                freezeTime += Time.deltaTime;

                if(freezeTime >= timeToWaitAfterTopColliderHit)
                {
                    freezeTime = 0;
                    hasHitTopCollider = false;
                    EnablePlayerInput();
                }
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == Tags.TopCollider)
            {
                playerLifeManager.DecreaseLife();
                ResetPlayerPosition();
                hasHitTopCollider = true;
                DisablePlayerInput();
            }
        }

        void DisablePlayerInput()
        {
            playerMovementInputController.enabled = false;
        }

        void EnablePlayerInput()
        {
            playerMovementInputController.enabled = true;
        }

        void ResetPlayerPosition()
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }
}
