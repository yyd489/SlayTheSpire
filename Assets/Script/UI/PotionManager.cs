using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class PotionManager : MonoBehaviour
    {
        [SerializeField] private GameObject popUsePotion;

        [SerializeField] PotionBase[] potionBases = new PotionBase[3];

        private List<Data.PotionType> listPotions = new List<Data.PotionType>();

        public void Init()
        {
            var dicPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().dicHavePotion;
            Data.PotionType potionType = Data.PotionType.Card;

            for (int i = 0; i < dicPotion.Count; i++)
            {
                potionBases[i].Init(potionType);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}