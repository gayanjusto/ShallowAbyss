using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.Shop;
using Assets.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Interfaces.UI;
using Assets.Scripts.Managers.Shop;

namespace Assets.Scripts.Entities
{
    public class ShopBuff : MonoBehaviour, IShopItem, ILanguageUI, IShopSelectedObject
    {
        public int buffAmount;
        public int buffPrice;
        public string buffName;

        public ShopSelectedObjectEnum buffType;

        protected ShopSceneManager shopSceneManager;
        protected PlayerStatusManager playerStatusManager;
        protected Color originalColor;
        protected SelectedObjectManager selectedObjectManager;

        protected void Start()
        {
            shopSceneManager = GameObject.Find("ShopSceneManager").GetComponent<ShopSceneManager>();
            playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            selectedObjectManager = GameObject.Find("SelectedObjectManager").GetComponent<SelectedObjectManager>();

            LoadTextsLanguage();
            originalColor = GetComponent<Image>().color;

            //set listener when clicking to select object
            GetComponent<Button>().onClick.AddListener(() => selectedObjectManager.SetSelectedObject(this));
        }

        public virtual Func<bool> HasEnoughCreditsToBuy()
        {
            throw new NotImplementedException();
        }

        public virtual Action BuyItem()
        {
            throw new NotImplementedException();
        }

        public virtual void LoadTextsLanguage()
        {
            throw new NotImplementedException();
        }

        public void SelectObject()
        {
            //Set gray color
            GetComponent<Image>().color = new Color(.5f, .5f, .5f);
        }

        public void DeselectObject()
        {
            GetComponent<Image>().color = originalColor;
        }

        public string GetName()
        {
            return this.buffName;
        }

        public ShopSelectedObjectEnum GetObjectType()
        {
            return buffType;
        }

        public int GetPrice()
        {
            return buffPrice;
        }
    }
}
