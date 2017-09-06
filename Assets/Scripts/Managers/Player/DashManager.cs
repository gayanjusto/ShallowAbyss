using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class DashManager : MonoBehaviour
    {
        public PlayerLifeManager playerLifeManager;
        public SpriteRenderer spriteRenderer;
        public PlayerMovementInputController playerMovementInputController;

        public GameObject dashParticleGameObject;
        public ParticleSystem dashParticle;
        public Slider dashSlider;
        public GameObject dashBtn;

        public float dashCoolDownTime;
        float dashCoolDownTickTime;

        public float dashBoost;
        public float dashDuration;
        float dashDurationTickTime;

        bool isDashing;
        bool isCoolingDownDash;

        void Start()
        {
            int dashUpgrades = PlayerStatusManager.PlayerDataInstance.GetDashUpgrade();

            if (dashUpgrades > 0)
            {
                dashDuration *= dashUpgrades;
            }
        }
        void Update()
        {
            //Check if dash is cooling down or cooldown time is up
            CheckDashCoolDown();

            if (isDashing)
            {
                CountDownDashTime();
            }
        }

        void CheckDashCoolDown()
        {
            if (isCoolingDownDash)
            {
                float timeToRenderFrame = Time.deltaTime;
                dashCoolDownTickTime += timeToRenderFrame;

                var amountIterationsLeft = (dashCoolDownTime - dashCoolDownTickTime) / timeToRenderFrame;
                var slidingAmount = (1 - dashSlider.value) / amountIterationsLeft;

                dashSlider.value += slidingAmount;

            }

            if (dashCoolDownTickTime >= dashCoolDownTime)
            {
                dashBtn.gameObject.SetActive(true);

                isCoolingDownDash = false;
                dashCoolDownTickTime = 0;
            }
        }

        void CountDownDashTime()
        {
            float timeToRenderFrame = Time.deltaTime;
            dashDurationTickTime += timeToRenderFrame;

            var amountIterationsLeft = (dashDuration - dashDurationTickTime) / timeToRenderFrame;
            var slidingAmount = dashSlider.value / amountIterationsLeft;

            dashSlider.value -= slidingAmount;

            if (dashDurationTickTime >= dashDuration)
            {
                isCoolingDownDash = true;
                playerMovementInputController.mov_boost_value = 1;

                dashDurationTickTime = 0;
                isDashing = false;
                dashParticleGameObject.SetActive(false);


                GetComponent<Collider2D>().enabled = true;
                Color originalColor = spriteRenderer.color;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            }
        }

        public void Dash()
        {
            playerMovementInputController.mov_boost_value = dashBoost;
            isDashing = true;
            playerLifeManager.SetPlayerInvulnerable();
            GetComponent<Collider2D>().enabled = false;

            dashBtn.gameObject.SetActive(false);
            dashParticleGameObject.SetActive(true);

            //set player transparent
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, .3f);
            dashParticle.Play();
        }
    }
}
