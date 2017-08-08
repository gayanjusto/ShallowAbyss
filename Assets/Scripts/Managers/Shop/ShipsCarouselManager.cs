using Assets.Scripts.Entities;
using UnityEngine;
using UnityEngine.UI;

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



        private void Start()
        {
            playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();

            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            //Get all ships available from repository
            GameObject[] ships = Resources.LoadAll<GameObject>("Prefabs/Ships");

            RectTransform containerRT = containerCarousel.GetComponent<RectTransform>();
            float containerHeight = containerRT.sizeDelta.y;

            //Set all ships in the containerCarousel
            //-> For each iteration, add the 'X' offset between ships options
            foreach (var ship in ships)
            {
                currentOffset_X += shipsOffset_X;
                GameObject shipInstance = Instantiate(ship);
                shipInstance.transform.SetParent(containerCarousel.transform);
                RectTransform shipRT = shipInstance.GetComponent<RectTransform>();
                shipRT.anchoredPosition = new Vector2(currentOffset_X, shipPosition_Y);

                containerRT.sizeDelta = new Vector2(containerRT.sizeDelta.x + containerOffset_X, containerHeight);

                //if player has already bought this ship, disable its button
                if (playerData.shipsOwnedIds.Contains(shipInstance.GetComponent<ShipCarousel>().shipId))
                {
                    DisableShipButtonClick(shipInstance);
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
    }
}
