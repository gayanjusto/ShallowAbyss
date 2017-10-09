using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class EnemyHeavyWhaleController : EnemyController
    {
        public SpriteRenderer bodyRenderer, tailRenderer;

        protected override void FlipSprite(bool flipX)
        {
            bodyRenderer.flipX = flipX;
            tailRenderer.flipX = flipX;

            if (tailRenderer.flipX)
            {
                tailRenderer.transform.localPosition = new Vector2(1, 0);
            }else
            {
                tailRenderer.transform.localPosition = new Vector2(-1, 0);
            }
        }
    }
}
