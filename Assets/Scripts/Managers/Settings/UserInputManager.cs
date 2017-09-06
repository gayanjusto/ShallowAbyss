using UnityEngine;

namespace Assets.Scripts.Managers.Settings
{
    public class UserInputManager : MonoBehaviour
    {
        public GameObject joystickBtn;
        public GameObject upBtn;
        public GameObject downBtn;
        public GameObject leftBtn;
        public GameObject rightBtn;
        public GameObject dashBtn;

        public bool isUsingJoystick;
        Vector2 originalAnchoredPos;

        private void Start()
        {
            originalAnchoredPos = dashBtn.GetComponent<RectTransform>().anchoredPosition;
            isUsingJoystick = true;
        }

        public void ChangeUserInput()
        {
            if (isUsingJoystick)
            {
                ChangeToButtons();
                isUsingJoystick = false;
                return;
            }

            ChangeToJoystick();
            isUsingJoystick = true;
        }

        void ChangeToButtons()
        {
            joystickBtn.SetActive(false);

            upBtn.SetActive(true);
            downBtn.SetActive(true);
            leftBtn.SetActive(true);
            rightBtn.SetActive(true);

            var dashRT = dashBtn.GetComponent<RectTransform>();
            dashRT.anchoredPosition = new Vector2(0, 345);
        }

        void ChangeToJoystick()
        {
            joystickBtn.SetActive(true);

            upBtn.SetActive(false);
            downBtn.SetActive(false);
            leftBtn.SetActive(false);
            rightBtn.SetActive(false);

            var dashRT = dashBtn.GetComponent<RectTransform>();
            dashRT.anchoredPosition = originalAnchoredPos;
        }
    }
}
