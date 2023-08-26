using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class PotionBase : MonoBehaviour
    {
        private Data.PotionType potionType;
        public int itemIndex;

        public void Init()
        {
            var dicPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().dicHavePotion;

            //potionType = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().dicHavePotion[];
        }
    }
}