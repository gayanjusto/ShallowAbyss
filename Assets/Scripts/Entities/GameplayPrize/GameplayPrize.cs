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
        public Transform player;
        private void Update()
        {
            if (Vector2.Distance(this.transform.position, player.position) <= 1.3f)
                GetPrize();

            MovementService.TranslateObjectVertically(this.gameObject, 1, speed);

            if (this.transform.position.y > topCollider.transform.position.y)
            {
                DeactivatePrize();
            }
        }


        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    Debug.Log(collision.gameObject.name);

        //    if (collision.gameObject.tag == Tags.Player)
        //    {
        //        audioSource.Play();

        //        if (prizeType == PrizeType.Life)
        //        {
        //            collision.gameObject.GetComponent<PlayerLifeManager>().IncreaseLife();
        //            DeactivatePrize();
        //            return;
        //        }

        //        //if (prizeType == PrizeType.Shield)
        //        //{
        //        //    collision.gameObject.GetComponent<PlayerShieldManager>().IncreaseShield();
        //        //    DeactivatePrize();
        //        //    return;
        //        //}

        //        if (prizeType == PrizeType.Credits)
        //        {
        //            GameObject.Find("ScoreCounterManager").GetComponent<ScoreManager>().score += 10;
        //            DeactivatePrize();
        //            return;
        //        }
        //    }

        //}

        void GetPrize()
        {
            audioSource.Play();

            if (prizeType == PrizeType.Life)
            {
                player.gameObject.GetComponent<PlayerLifeManager>().IncreaseLife();
                DeactivatePrize();
                return;
            }

            if (prizeType == PrizeType.Credits)
            {
                GameObject.Find("ScoreCounterManager").GetComponent<ScoreManager>().score += 10;
                DeactivatePrize();
                return;
            }
        }
        void DeactivatePrize()
        {
            this.transform.parent = prizesPool;
            gameObject.SetActive(false);
        }
    }
}
