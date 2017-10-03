using Assets.Scripts.Entities.Player;
using Assets.Scripts.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers.Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public Text dontShowThisAgainTxt;
        public GameObject tutorialPanel;
        public Text paginationTxt;
        public GameObject[] pages;
        public Button pageForwardBtn;
        public Button pageBackBtn;
        public Toggle dontShowToggle;

        public int currentPage;
        protected int maxPages
        {
            get
            {
                return pages.Length;
            }
        }

        protected void ActivatePanel(bool dontShowPanel)
        {
            if (dontShowPanel)
            {
                tutorialPanel.SetActive(false);
                Time.timeScale = 1;
                return;
            }
            else
            {
                tutorialPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
        protected void Start()
        {
            SetCurrentPage();
        }
        protected void SetCurrentPage()
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if (i == currentPage)
                {
                    pages[i].gameObject.SetActive(true);
                    continue;
                }

                pages[i].gameObject.SetActive(false);
            }

            paginationTxt.text = string.Format("{0}/{1}", currentPage + 1, maxPages);
        }

        protected void PressOk(TutorialData tutorialData)
        {
            TutorialDataService.SaveTutorialData(tutorialData);
            Time.timeScale = 1;
            tutorialPanel.SetActive(false);
            this.gameObject.SetActive(false);
        }

        public void NextPage()
        {

            currentPage++;
            SetCurrentPage();

            pageBackBtn.gameObject.SetActive(true);

            if (currentPage + 1 >= maxPages)
            {
                pageForwardBtn.gameObject.SetActive(false);
            }
            else
            {
                pageForwardBtn.gameObject.SetActive(true);
            }
        }

        public void PreviousPage()
        {
            currentPage--;
            SetCurrentPage();

            pageForwardBtn.gameObject.SetActive(true);

            if (currentPage + 1 <= 1)
            {
                pageBackBtn.gameObject.SetActive(false);
            }
            else
            {
                pageBackBtn.gameObject.SetActive(true);
            }
        }
        
    }
}
