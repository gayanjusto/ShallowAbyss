using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PlayerCollisionManager : MonoBehaviour
    {
        public PlayerShieldManager playerShieldManager;
        public PlayerLifeManager playerLifeManager;

        public bool CanBeHit()
        {
            return playerLifeManager.CanBeHit() && playerShieldManager.CanBeHit();
        }
    }
}
