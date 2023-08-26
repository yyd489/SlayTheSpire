using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class PotionManager : MonoBehaviour
    {
        [SerializeField] private GameObject popUsePotion;
        [SerializeField] private GameObject[] potionBtn;

        private List<Data.PotionType> listPotions = new List<Data.PotionType>();
        private int selectPotionIndex;

        public void Init()
        {
            var dicPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().dicHavePotion;

            for (int i = 0; i < dicPotion.Count; i++)
            {
                listPotions.Add(dicPotion[i]);
            }
        }

        public void PopUpPotion(int potionIndex)
        {
            popUsePotion.transform.position = new Vector2(potionBtn[potionIndex].transform.position.x, popUsePotion.transform.position.y);
            selectPotionIndex = potionIndex;
            popUsePotion.SetActive(true);
        }

        public void UsePotion()
        {

        }
    }
}