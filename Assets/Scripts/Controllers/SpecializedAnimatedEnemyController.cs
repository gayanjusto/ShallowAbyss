using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class SpecializedAnimatedEnemyController : EnemyController
    {

        protected override void GetSpriteRenderer()
        {
            spriteRenderer = this.transform.FindChild("SpriteRenderer").GetComponent<SpriteRenderer>();
        }
    }
}
