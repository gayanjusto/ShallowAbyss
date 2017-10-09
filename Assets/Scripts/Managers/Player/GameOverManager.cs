using Assets.Scripts.Entities.Ads;
using Assets.Scripts.Entities.Player;
using Assets.Scripts.Managers.Ads;
using Assets.Scripts.Managers.UI;
using Assets.Scripts.Services;
using Assets.Scripts.Services.AdMob;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public AudioSource prizeAudioSource, scoreCounterAudioSource;
        public ScoreManager scoreCounterManager;
        public AdsManager adsManager;
        public Image gameOverPanel;
        public Button dashBtn;
        public Slider dashSlider;
        public GameObject enemySpanwer, prizeSpawner, btnUp, btnDown, btnLeft, btnRight;
        public PrizeResultManager prizeResultManager;

        public Text gameOverScoreText;
        public Button adsButton;

        public Button pauseButton;

        public GameObject screenShotScorePanel;
        public Text screenShotScore;

        public bool gameHasFinished;
        public int gameOverScore;
        const string gameOverImage = "gameOver.png";
        int finalScore;

        void StartScoreCountDown(int finalScore)
        {
            scoreCounterAudioSource.Play();
            this.finalScore = finalScore;
            StartCoroutine(ScoreCountDownCoroutine(finalScore, false));
        }

        IEnumerator ScoreCountDownCoroutine(int finalScore, bool isAdsBonus)
        {
            while (scoreCounterManager.score > 0)
            {
                yield return new WaitForSeconds(0.05f);

                scoreCounterManager.DecreaseScore();

                //Make sure the final score displayed is the same persisted in player status
                if (scoreCounterManager.score <= 1)
                {
                    scoreCounterManager.ZeroScore();
                    if (isAdsBonus)
                    {
                        gameOverScore = this.finalScore + finalScore;
                    }
                    else
                    {
                        gameOverScore = finalScore;
                    }
                }
                else
                {
                    gameOverScore++;
                }
                gameOverScoreText.text = gameOverScore.ToString();
            }

            scoreCounterAudioSource.Stop();
        }

        void SaveGameTimeExperience()
        {
            var enemySpawnerManager = enemySpanwer.GetComponent<EnemySpawnerManager>();
            var levels = enemySpawnerManager.currentLevelDifficult;
            var difficultTime = enemySpawnerManager.currentDifficultTime;

            var totalTime = (levels * enemySpawnerManager.timeToChangeDifficult) + difficultTime;

            PlayerTimeExperienceDataService.SaveTimeExperience(totalTime);
        }

        public void SetGameOver(int finalScore)
        {
            enemySpanwer.SetActive(false);

            //Save game time experience
            SaveGameTimeExperience();

            dashBtn.gameObject.SetActive(false);
            dashSlider.gameObject.SetActive(false);

            enemySpanwer.SetActive(false);
            prizeSpawner.SetActive(false);
            btnUp.SetActive(false);
            btnDown.SetActive(false);
            btnLeft.SetActive(false);
            btnRight.SetActive(false);
            pauseButton.gameObject.SetActive(false);


            screenShotScore.text = finalScore.ToString();

            StartCoroutine(ShowGameOverPanel(finalScore));
        }

        IEnumerator ShowGameOverPanel(int finalScore)
        {
            yield return new WaitForSeconds(1);

            //Wait to show gameover panel and final score
            gameOverPanel.gameObject.SetActive(true);

            adsManager.WillShowBannerAds();
            adsButton.gameObject.SetActive(adsManager.CanShowVideoAds());
            StartScoreCountDown(finalScore);
        }

        public void TakeScreenShot()
        {
            //SetLayoutForScreenShot();

            //Capture screenshot
            Application.CaptureScreenshot(gameOverImage);

            //ResetLayoutAfterScreenShot();
        }
        public void ShareScreenShot()
        {
            SetLayoutForScreenShot();

            //Stop coroutine
            StopCoroutine("ScoreCountDownCoroutine");

            //Set finalScore for screenshot
            gameOverScoreText.text = this.finalScore.ToString();

            string screenShotPath = Application.persistentDataPath + "/" + gameOverImage;


            ////Capture screenshot
            //Application.CaptureScreenshot(gameOverImage);

            ResetLayoutAfterScreenShot();

            if (Application.platform == RuntimePlatform.Android)
            {
                OpenAndroidIntent(screenShotPath, LanguageService.GetLanguageDictionary().shareMsg);
            }

        }

        void OpenAndroidIntent(string screenShotPath, string shareMsg)
        {
            //instantiate the class Intent
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");

            //instantiate the object Intent
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");

            //call setAction setting ACTION_SEND as parameter
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //instantiate the class Uri
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");

            ////instantiate the object Uri with the parse of the url's file
            //AndroidJavaObject fileObject = new AndroidJavaObject("java.io.File", "file://" + screenShotPath);
            //AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("fromFile", fileObject);
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + screenShotPath);
            //bool fileExist = fileObject.Call<bool>("exists");
            //Debug.Log("File exist : " + fileExist);
            // Attach image to intent
            // if (fileExist)
            //{
            //call putExtra with the uri object of the file
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);
            //}

            //set the type of file
            intentObject.Call<AndroidJavaObject>("setType", "image/*");

            //instantiate the class UnityPlayer
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");

            //instantiate the object currentActivity
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            //call the activity with our Intent
            //currentActivity.Call("startActivity", intentObject);
            AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, shareMsg);
            currentActivity.Call("startActivity", jChooser);
        }

        void SetLayoutForScreenShot()
        {
            gameOverPanel.gameObject.SetActive(false);
            screenShotScorePanel.SetActive(true);
        }

        void ResetLayoutAfterScreenShot()
        {
            gameOverPanel.gameObject.SetActive(true);
            screenShotScorePanel.SetActive(false);
        }


        public void GiveAdsPrize()
        {
            var prize = AdsPrizeService.GetRandomPrize(this.finalScore);
            prize.GivePrize(this);
            PlayerStatusService.SavePlayerStatus(PlayerStatusService.LoadPlayerStatus());
            prizeAudioSource.Play();
        }

        public int GetFinalScore()
        {
            return this.finalScore;
        }

        public void SetPrizeMessage(AdsPrizeData prizeData)
        {
            prizeResultManager.gameObject.SetActive(true);
            prizeResultManager.SetPrizeImage(prizeData.prizeSprite);
            prizeResultManager.SetPrizeMessage(prizeData.message);
        }

        public void RollScoreForAdsCredits(int bonusScore)
        {
            scoreCounterManager.score = bonusScore;
            scoreCounterAudioSource.Play();
            StartCoroutine(ScoreCountDownCoroutine(bonusScore, true));
        }
    }
}
