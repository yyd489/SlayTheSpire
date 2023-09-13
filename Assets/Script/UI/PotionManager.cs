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
            if (GameManager.Instance.playerControler.onDrag) return;

            potionBtn[potionIndex] = GameManager.Instance.ingameTopUI.potionsPanel.GetChild(potionIndex).gameObject;
            popUsePotion.transform.position = new Vector2(potionBtn[potionIndex].transform.position.x, popUsePotion.transform.position.y);
            popUsePotion.SetActive(true);
            selectPotionIndex = potionIndex;
        }

        public void UsePotion()
        {
            var potionData = GameManager.Instance.dataManager.data.itemData.itemData.listPotionData;

            Data.PotionType[] arrPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;
            
            switch (arrPotion[selectPotionIndex])
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
            Data.PotionType[] arrPorion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;
            potionBtn[selectPotionIndex].SetActive(false);
            arrPorion[selectPotionIndex] = Data.PotionType.None;

            if (popUsePotion.activeSelf) CanclePopPotionUI();
        }

        public void CanclePopPotionUI()
        {
            popUsePotion.SetActive(false);
        }
    }
}