using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class BackgroundPropManager : MonoBehaviour
    {
        public BackgroundManager_new backgroundManager;

        public float offSetAfterLimit;
        public float minSpeed;

        float leftEdge;
        float  rightEdge;
        float topEdge;
        float bottomEdge;

        int horizontalDirection;

        float speed;
        Transform originalBackgroundContext;

        private void Start()
        {
            leftEdge = ScreenPositionService.GetLeftEdge(Camera.main).x;
            rightEdge = ScreenPositionService.GetRightEdge(Camera.main).x;
            topEdge = ScreenPositionService.GetTopEdge(Camera.main).y;
            bottomEdge = ScreenPositionService.GetBottomEdge(Camera.main).y;

            SetPosition();
        }

        private void Update()
        {
            MovementService.TranslateObjectHorizontally(this.gameObject, horizontalDirection, speed);

            if (horizontalDirection == 1 && transform.position.x > rightEdge + offSetAfterLimit)
            {
                RecallProp();
            }

           if(horizontalDirection == -1 && transform.position.x < leftEdge - offSetAfterLimit)
            {
                RecallProp();
            }
        }

        bool IsBeingRecalled()
        {
            return backgroundManager.RecallProps();
        }

        void RecallProp()
        {
            this.gameObject.SetActive(false);
            this.transform.parent = originalBackgroundContext;
            backgroundManager.DisableProp(this.gameObject);
            return;
        }

        public void SetPosition()
        {

            speed = RandomValueTool.GetRandomFloatValue(minSpeed, minSpeed * 3);

            float randomPos_Y = RandomValueTool.GetRandomFloatValue(bottomEdge, topEdge);
            float pos_X;
            int side = RandomValueTool.GetRandomValue(0, 100);

            //From left to right?
            if (side > 50)
            {
                horizontalDirection = 1;
                pos_X = RandomValueTool.GetRandomFloatValue(leftEdge * 2, leftEdge);
                GetComponent<SpriteRenderer>().flipX = true;
            }else
            {
                horizontalDirection = -1;
                pos_X = RandomValueTool.GetRandomFloatValue(rightEdge, rightEdge * 2);
                GetComponent<SpriteRenderer>().flipX = false;
            }

            this.transform.position = new Vector3(pos_X, randomPos_Y, 1);
        }

        public void SetOriginalContext(Transform context)
        {
            this.originalBackgroundContext = context;
        }

        public Transform GetOriginalContext()
        {
            return this.originalBackgroundContext;
        }
    }
}
