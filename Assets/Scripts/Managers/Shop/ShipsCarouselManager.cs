using Assets.Scripts.Entities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.Entities.Player;

namespace Assets.Scripts.Managers.Shop
{
    public class ShipsCarouselManager : MonoBehaviour
    {
        public PlayerStatusManager playerStatusManager;
        public GameObject containerCarousel;
        public float shipsOffset_X;
        public float shipPosition_Y;
        public float containerOffset_X;

        float currentOffset_X;
        GameObject[] shipsInstance;
        ShipCarousel defaultShip;

        void Start()
        {
            playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
        }

        GameObject[] GetAllShipsFromPrefabList()
        {
            //Get all ships available from repository
            GameObject[] ships = Resources.LoadAll<GameObject>("Prefabs/Ships");

            return ships;
        }

        void PlotShipsIntoCarousel(GameObject[] ships, bool displayPriceTag)
        {
            RectTransform containerRT = containerCarousel.GetComponent<RectTransform>();
            float containerHeight = containerRT.sizeDelta.y;
            shipsInstance = new GameObject[ships.Length];

            //Set all ships in the containerCarousel
            //-> For each iteration, add the 'X' offset between ships options
            for (int i = 0; i < ships.Length; i++)
            {
                currentOffset_X += shipsOffset_X;
                GameObject shipInstance = Instantiate(ships[i]);

                if (!displayPriceTag)
                {
                    shipInstance.transform.FindChild("PriceTag").gameObject.SetActive(false);
                }

                shipInstance.transform.SetParent(containerCarousel.transform);
                RectTransform shipRT = shipInstance.GetComponent<RectTransform>();
                shipRT.anchoredPosition = new Vector2(currentOffset_X, shipPosition_Y);

                containerRT.sizeDelta = new Vector2(containerRT.sizeDelta.x + containerOffset_X, containerHeight);
                shipsInstance[i] = shipInstance;

                //set defaultShip
                if(i == 0)
                {
                    defaultShip = shipInstance.GetComponent<ShipCarousel>();
                }
            }
        }

        void DisableOwnedShips()
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            foreach (var shipInstance in shipsInstance)
            {
                //if player has already bought this ship, disable its button
                if (playerData.shipsOwnedIds.Contains(shipInstance.GetComponent<ShipCarousel>().shipId))
                {
                    DisableShipButtonClick(shipInstance);
                }
            }
        }

        public void LoadAllShipsInCarousel(bool displayPriceTag)
        {
            GameObject[] ships = GetAllShipsFromPrefabList();
            PlotShipsIntoCarousel(ships, displayPriceTag);
            DisableOwnedShips();
        }

        public void LoadOwnedShips(bool displayPriceTag)
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            GameObject[] ships = GetAllShipsFromPrefabList();
            GameObject[] filteredList = ships.Where(x =>
            playerData.shipsOwnedIds.Contains(x.GetComponent<ShipCarousel>().shipId)).ToArray();
            PlotShipsIntoCarousel(filteredList, displayPriceTag);
        }

        public void HideNotOwnedShips()
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            List<int> ownedShipsIDs = playerData.shipsOwnedIds;

            foreach (var shipInstance in shipsInstance)
            {
                //if player doesn't have a ship, remove it from the interface
                if (!ownedShipsIDs.Contains(shipInstance.GetComponent<ShipCarousel>().shipId))
                {
                    shipInstance.SetActive(false);
                }
            }
        }

        public void DisableShipButtonClick(GameObject shipButtonParent)
        {
            Transform shipTransform = shipButtonParent.transform.FindChild("ShipButton");
            shipTransform.GetComponent<Button>().enabled = false;

            Image shipImage = shipTransform.GetComponent<Image>();
            Color shipOriginalColor = shipImage.color;
            shipImage.color = new Color(shipOriginalColor.r, shipOriginalColor.g, shipOriginalColor.b, .1f);
        }

        public ShipCarousel GetDefaultShip()
        {
            return defaultShip;
        }
    }
}
