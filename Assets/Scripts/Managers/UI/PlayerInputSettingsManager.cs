using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Managers.UI
{
    public class PlayerInputSettingsManager : MonoBehaviour
    {
        public GameObject tabletControls;
        public GameObject phoneControls;

        bool hasTabletActive;

        private void Start()
        {
            if (ScreenSizeService.DeviceIsTablet())
            {
                SetTabletControlsActive();
            }
        }

        public void ChangeControls()
        {
            if (hasTabletActive)
            {
                SetPhoneControlsActive();
            }else
            {
                SetTabletControlsActive();
            }
        }

        void SetPhoneControlsActive()
        {
            tabletControls.SetActive(false);
            phoneControls.SetActive(true);
            hasTabletActive = false;
        }
        void SetTabletControlsActive()
        {
            tabletControls.SetActive(true);
            phoneControls.SetActive(false);
            hasTabletActive = true;
        }
    }
}
