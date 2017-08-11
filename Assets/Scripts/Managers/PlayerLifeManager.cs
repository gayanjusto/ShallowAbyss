using Assets.Scripts.Entities.Player;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Managers
{
    public class PlayerLifeManager : MonoBehaviour
    {
        public int lifes;
        public int maxLifes;
        public List<Image> lifeImages;
        public Image initialLifeImage;
        public Image gameOverPanel;
        public float lifeImageOffset_x;
        public ScoreCounterManager scoreCounterManager;
        public PlayerStatusManager playerStatusManager;
        bool canBeHit;

        const string gameOverImage = "gameOver.png";

        private void Start()
        {
            PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();

            //Player has bought life buffs?
            if (playerData.lifeBuff > 0)
            {
                maxLifes += playerData.lifeBuff;
                lifes += playerData.lifeBuff;
            }

            lifeImages = new List<Image>();
            lifeImages.Add(initialLifeImage);

            canBeHit = true;
            float pos_x = initialLifeImage.rectTransform.anchoredPosition.x;
            float pos_y = initialLifeImage.rectTransform.anchoredPosition.y;

            for (int i = 1; i < maxLifes; i++)
            {
                Image lifeImage = Instantiate(initialLifeImage);
                lifeImage.transform.SetParent(initialLifeImage.transform.parent, false);
                float newPosX = (pos_x * (i + 1)) + (lifeImageOffset_x * i);
                lifeImage.rectTransform.anchoredPosition = new Vector2(newPosX, pos_y);
                lifeImages.Add(lifeImage);
            }
        }

        public void DecreaseLife()
        {
            if (canBeHit)
            {
                lifes--;

                //Game Over
                if (lifes == 0)
                {
                    string screenShotPath = Application.persistentDataPath + "/" + gameOverImage;

                    //Capture screenshot
                    Application.CaptureScreenshot(gameOverImage);

                    //execute the below lines if being run on a Android device
                    AndroidShareImage(screenShotPath);

                    //Stop the score count
                    scoreCounterManager.enabled = false;

                    //Save player stats
                    //Turn life and shield buffs equals zero after death
                    PlayerStatusData playerData = playerStatusManager.LoadPlayerStatus();
                    playerData.lifeBuff = 0;
                    playerData.shieldBuff = 0;
                    playerData.score += Mathf.FloorToInt(scoreCounterManager.score);
                    playerStatusManager.SavePlayerStatus(playerData);

                    gameOverPanel.gameObject.SetActive(true);

                    this.gameObject.SetActive(false);
                    return;
                }

                for (int i = lifeImages.Count - 1; i > 0; i--)
                {
                    if (lifeImages[i].IsActive())
                    {
                        lifeImages[i].gameObject.SetActive(false);
                        break;
                    }
                }


                //Make player invulnerable for few seconds
                StartCoroutine(MakePlayerInvulnerable());
            }

        }

        public bool CanBeHit()
        {
            return canBeHit;
        }

        IEnumerator MakePlayerInvulnerable()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
        }

        void AndroidShareImage(string screenShotPath)
        {
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
    }
}
