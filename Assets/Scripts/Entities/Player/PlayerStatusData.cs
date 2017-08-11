using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Entities.Player
{
    [Serializable]
    public class PlayerStatusData
    {
        public PlayerStatusData(int score, int lifeBuff, int shieldBuff)
        {
            this.score = score;
            this.lifeBuff = lifeBuff;
            this.shieldBuff = shieldBuff;
            this.shipsOwnedIds = new List<int>();
        }

        public int score;
        public int lifeBuff;
        public int shieldBuff;
        public List<int> shipsOwnedIds;
        public string shipSpritePath;
    }
}
