using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
using Assets.Scripts.Managers.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Entities
{
    public class ShipCarousel : MonoBehaviour
    {
        public Sprite shipImage;
        public string shipDescription;
        public int shipPrice;
        public int shipId;
        public string shipSpritePath;
        public Text priceTag;

        Text ui_shipDescription;
        ObjectSelectorManager objectSelectorManager;

        private void Start()
        {
            priceTag.text = string.Format("$ {0}", shipPrice);
            objectSelectorManager = GameObject.Find("ObjectSelectorManager").GetComponent<ObjectSelectorManager>();
            ui_shipDescription = GameObject.Find("SelectedItemDescription").GetComponent<Text>();

            try
            {
                var child = this.transform.FindChild("ShipButton");
                var cmp = child.gameObject.GetComponentInChildren<Image>();
                cmp.sprite = shipImage;
            }
            catch 
            {
                Debug.Log("Image component error");
            }
        
        }

        public void SelectShip()
        {
            //Set ship description
            ui_shipDescription.text = this.shipDescription;
            objectSelectorManager.SetSelectedObject(ShopSelectedObjectEnum.Ship, this.transform.gameObject, shipDescription);
        }
    }
}
