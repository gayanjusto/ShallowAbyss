using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Player;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerShipManager : MonoBehaviour
    {
        public Animator playerAnimator;

        private void Start()
        {
            playerAnimator = GetComponent<Animator>();
            PlayerStatusData playerData = PlayerStatusService.LoadPlayerStatus();

            int selectedShipId = playerData.GetSelectedShipId();
            if (selectedShipId == 0)
            {
                selectedShipId = 1;
            }
            GameObject loadedShipPrefab = Resources.Load<GameObject>("Prefabs/Ships/" + selectedShipId);
            Ship ship = loadedShipPrefab.GetComponent<Ship>();
            //Load propeller
            GameObject propeller = Instantiate(Resources.Load<GameObject>(ship.propellerPath));
            propeller.transform.parent = this.transform;

            //Load props
            for (int i = 0; i < ship.propsPath.Length; i++)
            {
                var prop = Instantiate(Resources.Load<GameObject>(ship.propsPath[i]));
                prop.transform.parent = this.transform;
            }

            //Load all sprite renderers into sprites manager
            GetComponent<PlayerSpritesManager>().LoadSpriteRenderers();
        }


    }
}
