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
            listPotions.Add(Data.PotionType.Heal);
            for (int i = 0; i < listPotions.Count; i++)
            {
                potionBtn[i].SetActive(true);
            }
        }

        public void PopUpPotionUi(int potionIndex)
        {
            if (GameManager.Instance.playerControler.onDrag) return;

            popUsePotion.transform.position = new Vector2(potionBtn[potionIndex].transform.position.x, popUsePotion.transform.position.y);
            selectPotionIndex = potionIndex;
            popUsePotion.SetActive(true);
        }

        public void UsePotion()
        {
            var potionData = GameManager.Instance.dataManager.data.itemData.itemData.listPotionData;

            switch (listPotions[selectPotionIndex])
            {
                case Data.PotionType.Fire:
                    for (int i = 0; i < potionData.Count; i++)
                    {
                        if (potionData[i].itemType == Data.PotionType.Fire)
                        {
                            GameManager.Instance.playerControler.SelectPorionData(potionData[i]);
                            break;
                        }
                    }
                    break;

                case Data.PotionType.Heal:
                    for (int i = 0; i < potionData.Count; i++)
                    {
                        Debug.Log(potionData[i].itemType);
                        if (potionData[i].itemType == Data.PotionType.Heal)
                        {
                            Debug.Log(potionData[i].potionEffect);
                            GameManager.Instance.playerControler.ironclad.Heal(potionData[i].potionEffect);
                    
                            DropPotion();
                            break;
                        }
                    }
                    break;

                case Data.PotionType.Card:
                    for (int i = 0; i < 2; i++)
                    {
                        GameManager.Instance.cardManager.DrawCard();
                    }
                    GameManager.Instance.cardManager.DefaltCardSorting();
                    DropPotion();
                    break;
            }
            CancelPopPotionUI();
        }

        public void DropPotion()
        {
            potionBtn[selectPotionIndex].SetActive(false);
            listPotions.RemoveAt(selectPotionIndex);
            if (popUsePotion.activeSelf) CancelPopPotionUI();
        }

        public void CancelPopPotionUI()
        {
            popUsePotion.SetActive(false);
        }
    }
}