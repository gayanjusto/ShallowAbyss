using Assets.Scripts.Entities.Player;
using Assets.Scripts.Managers.Ads;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public PlayerStatusManager playerStatusManager;
        public ScoreCounterManager scoreCounterManager;
        public AdsManager adsManager;
        public Image gameOverPanel;

        public Text gameOverScoreText;
        public Button adsButton;

        public Button pauseButton;

        public bool gameHasFinished;
        public int gameOverScore;
        const string gameOverImage = "gameOver.png";
        int finalScore;

        void StartScoreCountDown(int finalScore)
        {
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
                    }else
                    {
                        gameOverScore = finalScore;
                    }
                }
                else
                {
                    gameOverScore++;
                }
                gameOverScoreText.text =  gameOverScore.ToString();
            }
        }

        public void SetGameOver(int finalScore)
        {
            pauseButton.gameObject.SetActive(false);
            adsButton.gameObject.SetActive(adsManager.WillShowAdsButton());
            gameOverPanel.gameObject.SetActive(true);
            StartScoreCountDown(finalScore);
        }

        public void ShareScreenShot()
        {
            //Stop coroutine
            StopCoroutine("ScoreCountDownCoroutine");

            //Set finalScore for screenshot
            gameOverScoreText.text = this.finalScore.ToString();

            string screenShotPath = Application.persistentDataPath + "/" + gameOverImage;

            //Capture screenshot
            Application.CaptureScreenshot(gameOverImage);

#if UNITY_ANDROID
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
            currentActivity.Call("startActivity", intentObject);
#endif
        }

        public void GiveAdsPrize()
        {
            int bonusAmount = 100;
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
            playerData.score += bonusAmount;
            scoreCounterManager.score = bonusAmount;
            playerStatusManager.SavePlayerStatus(playerData);

            StartCoroutine(ScoreCountDownCoroutine(bonusAmount, true));
        }
    }
}
