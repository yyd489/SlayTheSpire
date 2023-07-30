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
        private int energy;
        private bool isPlayerControl;

        public CharacterBase playerCharacter;
        public List<CharacterBase> enemyCharacters;

        [SerializeField] private TextMeshProUGUI energyText;
        [SerializeField] GameObject TurnEndBtn;


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
                    break;
                case BattleState.EnemyTurn:
                    battleState = BattleState.PlayerTurn;
                    break;
                case BattleState.EndBattle:
                    break;
            }

            if(battleState == BattleState.PlayerTurn)
            {
                TurnEndBtn.SetActive(true);
                energy = maxEnergy;
                energyText.text = energy + "/" + maxEnergy;
            }
            else
            {
                TurnEndBtn.SetActive(false);
            }
        }

        // 테스트용---------------------------------------------------------------------------------------------------------
        public async void PlayerAttack()
        {
            if (energy > 0 && !isPlayerControl && battleState == BattleState.PlayerTurn)
            {
                isPlayerControl = true;
                energy--;
                energyText.text = energy + "/" + maxEnergy;
                await playerCharacter.Attack(enemyCharacters[0]);
                isPlayerControl = false;
            }
        }

        private async UniTask EnemyTurn()
        {
            TimeSpan delayTime = TimeSpan.FromSeconds(1);
            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                await UniTask.Delay(delayTime);
                await enemyCharacters[i].Attack(playerCharacter);
                await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            }

            await UniTask.Delay(delayTime);
            TurnChange();
        }
    }
}