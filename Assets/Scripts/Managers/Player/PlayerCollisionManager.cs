using Assets.Scripts.Constants;
using Assets.Scripts.Services;
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

        public Transform topCollider;
        public Transform bottomCollider;

        Vector3 topEdge;
        Vector3 bottomEdge;
        Vector3 leftEdge;
        Vector3 rightEdge;


        public bool CanBeHit()
        {
            return playerLifeManager.CanBeHit() && playerShieldManager.CanBeHit();
        }

        private void Start()
        {
            topEdge = ScreenPositionService.GetTopEdge(Camera.main);
            bottomEdge = ScreenPositionService.GetBottomEdge(Camera.main);

            var originalLeftEdge = ScreenPositionService.GetLeftEdge(Camera.main);
            var originalRightEdge = ScreenPositionService.GetRightEdge(Camera.main);
            leftEdge = new Vector3(originalLeftEdge.x + 1.5f, originalLeftEdge.y);
            rightEdge = new Vector3(originalRightEdge.x - 1.5f, originalRightEdge.y);
        }

        private void Update()
        {

            CheckPlayerWithinBoundaries();
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

        void CheckPlayerWithinBoundaries()
        {
            //If player is dashing: cannot go off boundaries, otherwise it can go off screen
            if (this.transform.position.y >= topEdge.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, topEdge.y - .05f);
            }

            if (this.transform.position.y <= bottomEdge.y)
            {
                this.transform.position = new Vector3(this.transform.position.x, bottomEdge.y + .1f);
            }

            if (this.transform.position.x <= leftEdge.x)
            {
                this.transform.position = new Vector3(leftEdge.x + .1f, this.transform.position.y);
            }

            if (this.transform.position.x >= rightEdge.x)
            {
                this.transform.position = new Vector3(rightEdge.x - .1f, this.transform.position.y);
            }
        }
    }
}
