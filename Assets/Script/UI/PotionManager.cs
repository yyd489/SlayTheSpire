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
            listPotions = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHavePotion;
        }

        public void PopUpPotion(int potionIndex)
        {
            popUsePotion.transform.position = new Vector2(potionBtn[potionIndex].transform.position.x, popUsePotion.transform.position.y);
            selectPotionIndex = potionIndex;
            popUsePotion.SetActive(true);
        }

        public void UsePotion()
        {
            switch (listPotions[selectPotionIndex])
            {
                case Data.PotionType.Fire:
                    GameManager.Instance.playerControler.selectPotion = listPotions[selectPotionIndex];
                    break;

                case Data.PotionType.Heal:
                    GameManager.Instance.playerControler.ironclad.Heal(10);
                    break;

                case Data.PotionType.Card:
                    for (int i = 0; i < 2; i++)
                    {
                        GameManager.Instance.cardManager.DrawCard();
                    }
                    break;
            }
        }
    }
}