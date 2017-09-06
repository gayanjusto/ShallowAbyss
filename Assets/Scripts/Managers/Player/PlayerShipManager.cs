using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerShipManager : MonoBehaviour
    {
        public SpriteRenderer shipRenderer;
        const string defaultShipSprite = "Sprites/Standard Enemy Sprites/0";

        private void Start()
        {
            shipRenderer = GetComponent<SpriteRenderer>();
            PlayerStatusManager playerStatusManager = GameObject.Find("PlayerStatusManager").GetComponent<PlayerStatusManager>();
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            Sprite loadedShipSprite = Resources.Load<Sprite>(playerData.GetShipSpritePath());

            if(loadedShipSprite == null)
            {
                loadedShipSprite = Resources.Load<Sprite>(defaultShipSprite);
            }
            shipRenderer.sprite = loadedShipSprite;
        }


    }
}
