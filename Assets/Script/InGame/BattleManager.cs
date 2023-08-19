using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

namespace FrameWork
{
    enum BattleState
    {
        Ready,
        PlayerTurn,
        EnemyTurn,
        EndBattle
    }

    public class BattleManager : MonoBehaviour
    {
        private BattleState battleState;
        private int maxEnergy;
        public int energy;
        private bool isPlayerControl;

        public CharacterBase playerCharacter;
        public List<CharacterBase> enemyCharacters;

        [SerializeField] private GameObject TurnEndBtn;

        [SerializeField] private TextMeshProUGUI deckCount;
        [SerializeField] private TextMeshProUGUI useDeckCount;
        [SerializeField] private TextMeshProUGUI energyText;

        void Start()
        {
            maxEnergy = 3;
            energy = maxEnergy;
            isPlayerControl = false;
            battleState = BattleState.Ready;
            TurnChange();
        }

        public void TurnChange()
        {
            switch (battleState)
            {
                case BattleState.Ready:
                    battleState = BattleState.PlayerTurn;
                    break;
                case BattleState.PlayerTurn:
                    battleState = BattleState.EnemyTurn;
                    EnemyTurn();
                    RefreshBuff(playerCharacter);
                    GameManager.Instance.cardManager.RemovePlayerCard();
                    break;
                case BattleState.EnemyTurn:
                    battleState = BattleState.PlayerTurn;
                    GameManager.Instance.cardManager.ReloadPlayerCard();
                    for (int i = 0; i < enemyCharacters.Count; i++)
                    {
                        if (enemyCharacters[i].IsDead()) continue;

                        RefreshBuff(enemyCharacters[i]);
                    }
                    break;
                case BattleState.EndBattle:
                    break;
            }

            if(battleState == BattleState.PlayerTurn)
            {
                TurnEndBtn.SetActive(true);
                energy = maxEnergy;
                RefreshEnergyText();
            }
            else
            {
                TurnEndBtn.SetActive(false);
            }
        }

        public void RefreshBuff(CharacterBase character)
        {
            for (int i = 0; i < character.listBuff.Count; i++)
            {
                character.listBuff[i].turn--;

                // 버프아이콘 제거필요
                if (character.listBuff[i].turn == 0) character.listBuff.RemoveAt(i);
            }

            character.InputBuffStat();
        }

        public void RefreshDeckCountText(int deck, int useDeck)
        {
            deckCount.text = deck.ToString();
            useDeckCount.text = useDeck.ToString();
        }

        public void RefreshEnergyText(int useEnergy = 0)
        {
            energy -= useEnergy;
            energyText.text = string.Format("{0}/3", energy);
        }

        // 테스트용---------------------------------------------------------------------------------------------------------

        private async UniTask EnemyTurn()
        {
            TimeSpan delayTime = TimeSpan.FromSeconds(1);
            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                await UniTask.Delay(delayTime);
                await enemyCharacters[i].Attack(playerCharacter, 0, 0);
                await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            }

            await UniTask.Delay(delayTime);
            TurnChange();
        }
    }
}