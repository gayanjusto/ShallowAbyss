using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.UI
{
    public class ButtonFlagsManager : MonoBehaviour
    {
        Image mainBtn;
        Color currentMainBtnColor;

        private void Start()
        {
            mainBtn = this.transform.parent.GetComponent<Image>();
            currentMainBtnColor = mainBtn.color;
        }

        void Update()
        {
            if(mainBtn.color.r != currentMainBtnColor.r)
            {
                this.GetComponent<Image>().color = mainBtn.color;
                currentMainBtnColor = mainBtn.color;
            }
        }
    }
}
