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

        public void PopUpPotionUi(GameObject potionObject, int potionIndex)
        {
            if (GameManager.Instance.playerControler.onDrag) return;

            GameManager.Instance.potionManager.potionBtn[potionIndex] = potionObject;
            popUsePotion.transform.position = new Vector2(potionBtn[potionIndex].transform.position.x, popUsePotion.transform.position.y);
            popUsePotion.SetActive(true);
            selectPotionIndex = potionIndex;
        }

        public void UsePotion()
        {
            BattleState battleState = GameManager.Instance.battleManager.battleState;
            if (battleState == BattleState.EnemyTurn) return;

            var potionData = GameManager.Instance.dataManager.data.itemData.itemData.listPotionData;

            Data.PotionType[] arrPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;

            bool isBattle = GameManager.Instance.battleManager.IsBattleStage();

            switch (arrPotion[selectPotionIndex])
            {
                case Data.PotionType.Fire:
                    if (!isBattle || battleState != BattleState.PlayerTurn) return;

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
                    if (!isBattle || battleState != BattleState.PlayerTurn) return;

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
            Destroy(potionBtn[selectPotionIndex].gameObject);
            arrPorion[selectPotionIndex] = Data.PotionType.None;

            if (popUsePotion.activeSelf) CanclePopPotionUI();
        }

        public void CanclePopPotionUI()
        {
            popUsePotion.SetActive(false);
        }
    }
}