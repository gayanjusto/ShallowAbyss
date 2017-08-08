using Assets.Scripts.Enums;
using Assets.Scripts.Managers;
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

        Text ui_shipDescription;
        ShopSceneManager shopSceneManager;

        private void Start()
        {
            shopSceneManager = GameObject.Find("ShopSceneManager").GetComponent<ShopSceneManager>();
            ui_shipDescription = GameObject.Find("SelectedItenDescription").GetComponent<Text>();

            var child = this.transform.FindChild("ShipButton");
            var cmp = child.GetComponent<Image>();
            cmp.sprite = shipImage;
        }

        public void SelectShip()
        {
            //Set ship description
            ui_shipDescription.text = this.shipDescription;

            shopSceneManager.SetSelectedObject(ShopSelectedObjectEnum.Ship, this.transform.gameObject, shipDescription);
        }
    }
}
