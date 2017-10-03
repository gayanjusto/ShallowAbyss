using Assets.Scripts.Services;
using Assets.Scripts.Tools;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GamePlayPrizesManager : MonoBehaviour
    {
        public ScoreManager scoreManager;

        public GameObject prizesPool;

        public float prizeSpawnCoolDown;
        public float prizeCoolDownTickTime;
        public bool hasSpawnedPrize;

        public GameObject lifePrize;
        public GameObject shieldPrize;
        public GameObject creditsPrize;

        Vector3 leftEdge;
        Vector3 rightEdge;

        private void Start()
        {
            hasSpawnedPrize = true;
            leftEdge = ScreenPositionService.GetLeftEdge(Camera.main);
            rightEdge = ScreenPositionService.GetRightEdge(Camera.main);
        }

        private void Update()
        {
            if (hasSpawnedPrize)
            {
                prizeCoolDownTickTime += Time.deltaTime;

                if (prizeCoolDownTickTime >= prizeSpawnCoolDown)
                {
                    hasSpawnedPrize = false;
                    prizeCoolDownTickTime = 0;
                }
            }

            if (/*scoreManager.PlayerInRedZone() && */!hasSpawnedPrize)
            {

                //attempt to give prize
                int prizeChance = RandomValueTool.GetRandomValue(0, 100);

                if (prizeChance >= 75)
                {
                    hasSpawnedPrize = true;
                    GetPrizeToSpawn();
                }
            }
        }

        void GetPrizeToSpawn()
        {
            //prizes: dashCoolDown, life, shield, extra credits
            int prizeValue = RandomValueTool.GetRandomValue(0, 100);
            GameObject prize = null;

            if (prizeValue >= 90 && scoreManager.PlayerInRedZone())
            {
                //spawn life
                lifePrize.transform.parent = null;
                lifePrize.gameObject.SetActive(true);
                prize = lifePrize;
            }
            //else if (prizeValue < 90 && prizeValue >= 80)
            //{
            //    //spawn shield
            //    shieldPrize.transform.parent = null;
            //    shieldPrize.gameObject.SetActive(true);
            //    prize = shieldPrize;
            //}
            else if (prizeValue < 90 && prizeValue >= 0)
            {
                //spawn credits (50)
                creditsPrize.transform.parent = null;
                creditsPrize.gameObject.SetActive(true);
                prize = creditsPrize;
            }
            if (prize != null)
            {
                SetPrizePosition(prize);
                hasSpawnedPrize = true;
            }
        }

        void SetPrizePosition(GameObject prize)
        {
            float random_x_pos = RandomValueTool.GetRandomFloatValue(leftEdge.x + 1.5f, rightEdge.x - 1.5f);
            Vector3 prizePos = new Vector3(random_x_pos, prizesPool.transform.position.y, 0);

            prize.transform.position = prizePos;
        }
    }
}
