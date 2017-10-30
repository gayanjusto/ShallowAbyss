namespace Assets.Scripts.Controllers
{
    public class ChargerEnemyController : EnemyController
    {
        protected override void SetSpriteDirection()
        {
            FlipSprite(false);
        }
    }
}
