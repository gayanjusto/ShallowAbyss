using Assets.Scripts.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerSpritesManager : MonoBehaviour
    {
        SpriteRenderer[] spritesRenderers;

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
    }
}
