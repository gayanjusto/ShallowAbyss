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
        public bool canBeHit;
        public List<Image> lifeImages;
        public Image initialLifeImage;
        public Image gameOverPanel;
        public float lifeImageOffset_x;
        public ScoreCounterManager scoreCounterManager;
        public ScoreManager scoreManager;


        private void Start()
        {
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
                    string imageName = "gameOver.png";
                    string screenShotPath = Application.persistentDataPath + "/" + imageName;

                    //Capture screenshot
                    Application.CaptureScreenshot(screenShotPath);

                    //execute the below lines if being run on a Android device
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

                    //Stop the score count
                    scoreCounterManager.enabled = false;

                    //Save score
                    scoreManager.SaveScore(scoreCounterManager.score);

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

        IEnumerator MakePlayerInvulnerable()
        {
            canBeHit = false;
            yield return new WaitForSeconds(3);
            canBeHit = true;
        }
    }
}
