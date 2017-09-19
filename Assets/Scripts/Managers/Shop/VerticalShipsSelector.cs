using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Shop
{
    public class VerticalShipsSelector : MonoBehaviour
    {
        public GameObject containerCarousel;

        public float verticalOffset;

        public float objWidth;
        public float objHeight;

        [Range(0,1)]
        public float verticalSliderValue;

        Ship[] instantiatedShips;

        void Start()
        {
            var ships = GetAllShipsFromPrefabList();

            PlotShipsIntoCarousel(ships, true);
            DisableOwnedShips(instantiatedShips);

            SetVerticalSliderValue();
        }

        GameObject[] GetAllShipsFromPrefabList()
        {
            //Get all ships available from repository
            GameObject[] ships = Resources.LoadAll<GameObject>("Prefabs/Ships");
            return ships;
        }

        void DisableOwnedShips(Ship[] instantiatedShips)
        {
            //player data in ShipCarousel instance has not been loaded yet, so we'll have to do it manually
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            foreach (var shipInstance in instantiatedShips)
            {
                //if player has already bought this ship, disable its button
                if (playerData.GetOwnedShipsIDs().Contains(shipInstance.GetId()))
                {
                    shipInstance.DisableCharacterButton();
                }
            }
        }

        void SetVerticalSliderValue()
        {
            transform.FindChild("Scrollbar Vertical").GetComponent<Scrollbar>().value = verticalSliderValue;
        }
        public void PlotShipsIntoCarousel(GameObject[] ships, bool displayPriceTag)
        {
            instantiatedShips = new Ship[ships.Length];

            RectTransform containerRT = containerCarousel.GetComponent<RectTransform>();
            float containerWidth = containerRT.sizeDelta.x;
            containerRT.sizeDelta = new Vector2(containerWidth, (verticalOffset* (ships.Length +1)));

            float containerHeight = containerRT.sizeDelta.y;
            float startingPos_Y =  -verticalOffset;

            //Set all ships in the containerCarousel
            //-> For each iteration, add the vertical offset between ships options
            float currentOffset_v = startingPos_Y;
            for (int i = 0; i < ships.Length; i++)
            {
                GameObject shipInstance = Instantiate(ships[i]);
                instantiatedShips[i] = shipInstance.GetComponent<Ship>();
                if (!displayPriceTag)
                {
                    shipInstance.transform.FindChild("PriceTag").gameObject.SetActive(false);
                }

                shipInstance.transform.SetParent(containerCarousel.transform, false);
                RectTransform shipRT = shipInstance.GetComponent<RectTransform>();
                shipRT.anchoredPosition = new Vector2(0, currentOffset_v);

                currentOffset_v -= verticalOffset;
            }
        }

        
    }
}
