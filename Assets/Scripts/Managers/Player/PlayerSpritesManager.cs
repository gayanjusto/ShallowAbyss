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

        IEnumerator FlashPLayer()
        {
            while (!playerLifeManager.CanBeHit())
            {
                yield return new WaitForSeconds(0.1f);
                for (int i = 0; i < spritesRenderers.Length; i++)
                {
                    spritesRenderers[i].enabled = !spritesRenderers[i].enabled;
                }
            }

            if (playerLifeManager.CanBeHit())
            {
                for (int i = 0; i < spritesRenderers.Length; i++)
                {
                    spritesRenderers[i].enabled = true;
                }
                StopCoroutine(FlashPLayer());
            }
        }
    }
}
