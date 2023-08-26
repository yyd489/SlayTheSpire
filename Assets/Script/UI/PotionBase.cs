using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class PotionBase : MonoBehaviour
    {
        private Data.PotionType potionType;
        public int itemIndex;

        public void Init(Data.PotionType potionType)
        {
            this.potionType = potionType;
        }
    }
}