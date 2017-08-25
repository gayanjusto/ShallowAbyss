using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces.UI;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Managers.Shop;
using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;

namespace Assets.Scripts.Managers
{
    public class ShipSelectorManager : MonoBehaviour, IObjectSelector
    {
        public GameObject SelectedObject { get; set; }
        public Text SelectedObjectText { get; set; }
        public ShopSelectedObjectEnum ShopSelectedObjectEnum { get; set; }
        public ShipsCarouselManager shipsCarouselManager;
        ShipCarousel selectedShip;

        private void Start()
        {

            SelectedObjectText = GameObject.Find("SelectedItemDescription").GetComponent<Text>();
            shipsCarouselManager.LoadOwnedShips(false);

            selectedShip = shipsCarouselManager.GetDefaultShip();
        }

        public void SetSelectedObject(ShopSelectedObjectEnum selectedObj, GameObject selectedGameObj, string objectName)
        {
            this.ShopSelectedObjectEnum = selectedObj;
            this.SelectedObject = selectedGameObj;
            this.SelectedObjectText.text = objectName;

            selectedShip = this.SelectedObject.GetComponent<ShipCarousel>();
        }

        public void DefineShip()
        {
            PlayerStatusManager playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            PlayerStatusData playerdata = playerStatusManager.LoadPlayerStatus();
            string spritePath = selectedShip.shipSpritePath;

            playerdata.shipSpritePath = spritePath;

            playerStatusManager.SavePlayerStatus(playerdata);

            GameObject.Find("SceneManager").GetComponent<ScenesManager>().LoadNewGame();
        }
    }
}
