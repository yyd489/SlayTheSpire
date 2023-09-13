using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class PotionManager : MonoBehaviour
    {
        [SerializeField] private GameObject popUsePotion;
        [SerializeField] private GameObject[] potionBtn;

        private int selectPotionIndex;

        public void PopUpPotionUi(int potionIndex)
        {
            Debug.Log(potionIndex);
            if (GameManager.Instance.playerControler.onDrag) return;

            potionBtn[potionIndex] = GameManager.Instance.ingameTopUI.potionsPanel.GetChild(potionIndex).gameObject;
            popUsePotion.transform.position = new Vector2(potionBtn[potionIndex].transform.position.x, popUsePotion.transform.position.y);
            popUsePotion.SetActive(true);
            selectPotionIndex = potionIndex;
        }

        public void UsePotion()
        {
            var potionData = GameManager.Instance.dataManager.data.itemData.itemData.listPotionData;

            List<Data.PotionType> listPotions = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHavePotion;
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
            CanclePopPotionUI();
        }

        public void DropPotion()
        {
            List<Data.PotionType> listPotions = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHavePotion;
            potionBtn[selectPotionIndex].SetActive(false);
            listPotions.RemoveAt(selectPotionIndex);
            if (popUsePotion.activeSelf) CanclePopPotionUI();
        }

        public void CanclePopPotionUI()
        {
            popUsePotion.SetActive(false);
        }
    }
}