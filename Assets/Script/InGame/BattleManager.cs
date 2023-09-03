using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;

namespace FrameWork
{
    public enum BattleState
    {
        Ready,
        PlayerTurn,
        EnemyTurn,
        EndBattle
    }

    public class BattleManager : MonoBehaviour
    {
        public BattleState battleState;
        private int maxEnergy;
        public int energy;
        public bool firstTurn;

        public List<CharacterBase> enemyCharacters = new List<CharacterBase>();

        [SerializeField] private SpawnManager spawnManager;

        [SerializeField] private GameObject TurnEndBtn;
        [SerializeField] private TextMeshProUGUI deckCount;
        [SerializeField] private TextMeshProUGUI useDeckCount;
        [SerializeField] private TextMeshProUGUI energyText;

        [SerializeField] private TextMeshProUGUI narrationText;

        [SerializeField] private GameObject rewardPop;
        private IEnumerator coNarration;

        public void Init()
        {
            maxEnergy = 3;
            energy = maxEnergy;
            battleState = BattleState.Ready;
            TurnChange();
            //spawnManager.Init();            
        }

        public void TurnChange()
        {
            switch (battleState)
            {
                case BattleState.Ready:
                    firstTurn = true;
                    battleState = BattleState.PlayerTurn;
                    break;
                case BattleState.PlayerTurn:
                    if (firstTurn)
                    {
                        firstTurn = false;
                        GameManager.Instance.playerControler.ironclad.SetRelicStatus(firstTurn);
                    }
                    battleState = BattleState.EnemyTurn;
                    Narration("Enemy Turn");
                    RefreshBuff(GameManager.Instance.playerControler.playerCharacter);
                    EnemyTurn();
                    GameManager.Instance.cardManager.RemovePlayerCard();
                    break;
                case BattleState.EnemyTurn:
                    if (!GameManager.Instance.playerControler.playerCharacter.IsDead()) battleState = BattleState.PlayerTurn;
                    else battleState = BattleState.EndBattle;

                    GameManager.Instance.cardManager.ReloadPlayerCard();
                    for (int i = 0; i < enemyCharacters.Count; i++)
                    {
                        if (enemyCharacters[i].IsDead()) continue;

                        RefreshBuff(enemyCharacters[i]);
                    }
                    break;
                case BattleState.EndBattle:
                    if (!GameManager.Instance.playerControler.playerCharacter.IsDead())
                    {
                        var ralic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;
                        if (ralic.Contains(Data.RelicType.HealFire))
                            GameManager.Instance.playerControler.ironclad.Heal(10);
                    }
                    else
                    {
                        Instantiate(rewardPop);
                        Debug.Log("전투 종료");
                    }
                    break;
            }

            if(battleState == BattleState.PlayerTurn)
            {
                Narration("Player Turn");
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

        public void Narration(string text)
        {
            if (coNarration != null)
            {
                StopCoroutine(coNarration);
                coNarration = null;
            }

            coNarration = OnNarration(text);
            StartCoroutine(coNarration);
        }

        private IEnumerator OnNarration(string text)
        {
            narrationText.gameObject.SetActive(true);
            narrationText.text = text;
            narrationText.alpha = 0f;

            float alpha = 0.025f;

            while(narrationText.alpha < 1f)
            {
                narrationText.alpha += alpha;
                yield return null;
            }

            while (narrationText.alpha > 0f)
            {
                narrationText.alpha -= alpha;
                yield return null;
            }
        }

        // 테스트용---------------------------------------------------------------------------------------------------------

        private async UniTask EnemyTurn()
        {
            TimeSpan delayTime = TimeSpan.FromSeconds(1);
            bool isLosePlayer = false;
            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                await UniTask.Delay(delayTime);
                await enemyCharacters[i].Attack(GameManager.Instance.playerControler.playerCharacter, 0, 0);
                await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

                if (GameManager.Instance.playerControler.playerCharacter.IsDead())
                {
                    isLosePlayer = true;
                    break;
                }
            }

            if (!isLosePlayer)
                await UniTask.Delay(delayTime);

            TurnChange();
        }
    }
}