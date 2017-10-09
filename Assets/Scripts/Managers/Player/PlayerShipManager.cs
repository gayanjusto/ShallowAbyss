using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerShipManager : MonoBehaviour
    {
        public Animator playerAnimator;
        public Sprite playerPreviewSprite;

        protected virtual void Start()
        {
            playerAnimator = GetComponent<Animator>();

            Ship ship = GetAttachedShipScript();

            InstantiatePropeller(ship);

            InstantiateProps(ship);

            //Load all sprite renderers into sprites manager
            GetComponent<PlayerSpritesManager>().LoadSpriteRenderers();

            //Set body Sprite
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ship.bodyPath);
        }

        protected GameObject LoadShipPrefab()
        {
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            int selectedShipId = playerData.GetSelectedShipId();
            if (selectedShipId == 0)
            {
                selectedShipId = 1;
            }

            return Resources.Load<GameObject>("Prefabs/Ships/" + selectedShipId);
        }

        protected Ship GetAttachedShipScript()
        {
            return LoadShipPrefab().GetComponent<Ship>();
        }
        protected void InstantiatePropeller(Ship ship)
        {
            GameObject propeller = Instantiate(Resources.Load<GameObject>(ship.propellerPath), this.transform, false);
        }

        protected void InstantiateProps(Ship ship)
        {
            for (int i = 0; i < ship.propsPath.Length; i++)
            {
                var prop = Instantiate(Resources.Load<GameObject>(ship.propsPath[i]), this.transform, true);
                prop.transform.localPosition = new Vector3(prop.transform.localPosition.x, prop.transform.localPosition.y + this.transform.position.y, prop.transform.localPosition.z);
            }
        }

    }
}
