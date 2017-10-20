using Assets.Scripts.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerSpritesManager : MonoBehaviour
    {
        public SpriteRenderer[] spritesRenderers;
        PlayerLifeManager playerLifeManager;

        private void Start()
        {
             playerLifeManager = GetComponent<PlayerLifeManager>();
        }

        IEnumerator FlashPLayer()
        {
            while (!playerLifeManager.CanBeHit())
            {
                yield return new WaitForSeconds(0.1f);
                EnableFlashingSprites();
            }

            if (playerLifeManager.CanBeHit())
            {
                EnableSprites();
                StopCoroutine(FlashPLayer());
            }
        }

        void EnableFlashingSprites()
        {
            for (int i = 0; i < spritesRenderers.Length; i++)
            {
                spritesRenderers[i].enabled = !spritesRenderers[i].enabled;
            }
        }

        void EnableSprites()
        {
            for (int i = 0; i < spritesRenderers.Length; i++)
            {
                spritesRenderers[i].enabled = true;
            }
        }

        public void LoadSpriteRenderers()
        {
            var renderers = new List<SpriteRenderer>();

            //get sprite renderer of player's body
            renderers.Add(GetComponent<SpriteRenderer>());

            foreach (Transform child in transform)
            {
                if(child.tag == Tags.PlayerPropSprite)
                {
                    renderers.Add(child.GetComponent<SpriteRenderer>());
                }
            }

            spritesRenderers = renderers.ToArray();
            renderers = null;
        }

        public void FlipSprites_X()
        {
            for (int i = 0; i < spritesRenderers.Length; i++)
            {
                spritesRenderers[i].flipX = !spritesRenderers[i].flipX;
            }
        }

        public bool IsMovingLeft()
        {
            //if equals true then it's moving left (flipped X checked in Inspector)
            return spritesRenderers[0].flipX;
        }

        public void FlashSprites()
        {
            StartCoroutine(FlashPLayer());
        }

        public void DisableFlashSpriteCoroutine()
        {
            StopCoroutine(FlashPLayer());
            EnableSprites();
        }
      
        public void SetSpritesTransparent()
        {
            for (int i = 0; i < spritesRenderers.Length; i++)
            {
                Color originalColor = spritesRenderers[i].color;
                spritesRenderers[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, .3f);
            }
        }

        public void SetSpritesOpaque()
        {
            for (int i = 0; i < spritesRenderers.Length; i++)
            {
                Color originalColor = spritesRenderers[i].color;
                spritesRenderers[i].color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
            }
        }
    }
}
