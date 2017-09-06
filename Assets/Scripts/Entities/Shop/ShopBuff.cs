using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces.UI;

namespace Assets.Scripts.Entities
{
    public class ShopBuff : MonoBehaviour, IShopBuff, ILanguageUI
    {
        public int buffAmount;
        public int buffPrice;
        public string buffName;

        public ShopSelectedObjectEnum buffType;

        protected ShopSceneManager shopSceneManager;
        protected PlayerStatusManager playerStatusManager;

        protected void Start()
        {
             shopSceneManager = GameObject.Find("ShopSceneManager").GetComponent<ShopSceneManager>();
            playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            SetBtnListener();
            LoadTextsLanguage();
        }

        public virtual Func<bool> CanIncreaseBuff()
        {
            throw new NotImplementedException();
        }

        public virtual Action BuyBuff()
        {
            throw new NotImplementedException();
        }

        protected void SetBtnListener()
        {
            this.GetComponent<Button>().onClick.AddListener(() => shopSceneManager.SetSelectedObject(buffType, this.transform.gameObject, this.buffName));
        }

        public virtual void LoadTextsLanguage()
        {
            throw new NotImplementedException();
        }
    }
}
