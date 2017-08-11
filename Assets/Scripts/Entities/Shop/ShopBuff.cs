using Assets.Scripts.Entities.Player;
using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities
{
    public class ShopBuff : MonoBehaviour
    {
        public int buffAmount;
        public int buffPrice;
        public int maxBuffAmount;
        public string buffName;
        public string maxErrorMsg;

        public ShopSelectedObjectEnum buffType;

        private void Start()
        {
            ShopSceneManager shopSceneManager = GameObject.Find("ShopSceneManager").GetComponent<ShopSceneManager>();
            this.GetComponent<Button>().onClick.AddListener(() => shopSceneManager.SetSelectedObject(buffType, this.transform.gameObject, this.buffName));
        }

        public bool HasBoughtMaxAmount(PlayerStatusData playerStatus)
        {
            return playerStatus.lifeBuff + 1 > maxBuffAmount;
        }
    }
}
