using Assets.Scripts.Constants;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameplayPrize : MonoBehaviour
    {
        public GamePlayPrizesManager gamePlayPrizesManager;
        public GameObject topCollider;
        public Transform prizesPool;
        public PrizeType prizeType;
        public float speed;
        public AudioSource audioSource;

        private void Update()
        {

            MovementService.TranslateObjectVertically(this.gameObject, 1, speed);

            if (this.transform.position.y > topCollider.transform.position.y)
            {
                DeactivatePrize();
            }
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == Tags.Player)
            {
                audioSource.Play();

                if (prizeType == PrizeType.Life)
                {
                    collision.gameObject.GetComponent<PlayerLifeManager>().IncreaseLife();
                    DeactivatePrize();
                    return;
                }

                //if (prizeType == PrizeType.Shield)
                //{
                //    collision.gameObject.GetComponent<PlayerShieldManager>().IncreaseShield();
                //    DeactivatePrize();
                //    return;
                //}

                if (prizeType == PrizeType.Credits)
                {
                    GameObject.Find("ScoreCounterManager").GetComponent<ScoreManager>().score += 15;
                    DeactivatePrize();
                    return;
                }
            }

        }

        void DeactivatePrize()
        {
            this.transform.parent = prizesPool;
            gameObject.SetActive(false);
        }
    }
}
