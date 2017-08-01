using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class ShopSceneManager : MonoBehaviour
    {
        public ScoreManager scoreManager;
        public Text scoreAmount;

        private void Start()
        {
            scoreAmount.text = scoreManager.LoadScore().ToString();
        }
    }
}
