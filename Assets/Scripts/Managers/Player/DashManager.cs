using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class DashManager : MonoBehaviour
    {
        public PlayerLifeManager playerLifeManager;
        public SpriteRenderer spriteRenderer;
        public PlayerMovementInputController playerMovementInputController;

        public ParticleSystem dashParticle;
        public Slider dashSlider;
        public GameObject dashBtn;

        public float dashCoolDownTime;
        float dashCoolDownTickTime;

        public float dashDuration;
        float dashDurationTickTime;

        bool isDashing;
        bool hasUsedDash;
        bool isCoolingDownDash;

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
                hasUsedDash = false;
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
                playerMovementInputController.mov_buff_value = 1;

                dashDurationTickTime = 0;
                isDashing = false;
                GetComponent<Collider2D>().enabled = true;
                Color originalColor = spriteRenderer.color;
                spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            }
        }

        public void Dash()
        {
            playerMovementInputController.mov_buff_value = 3;
            isDashing = true;
            playerLifeManager.SetPlayerInvulnerable();
            GetComponent<Collider2D>().enabled = false;
            hasUsedDash = true;

            dashBtn.gameObject.SetActive(false);

            //set player transparent
            Color originalColor = spriteRenderer.color;
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, .3f);
            dashParticle.Play();
        }
    }
}
 