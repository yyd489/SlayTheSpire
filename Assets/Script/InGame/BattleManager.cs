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
    public enum MonsterAction
    {
        Attack,
        BuffSkill,
        DeBuffSkill
    }

    public class BattleManager : MonoBehaviour
    {
        public BattleState battleState;
        private int maxEnergy;
        public int energy;
        public bool firstTurn;

        [SerializeField] public List<CharacterBase> enemyCharacters = new List<CharacterBase>();

        public Sprite[] arrBuffIcon;
        public Sprite[] arrMonsterActionIcon;

        public ObjectPool hitEffectPool;
        [SerializeField] private SpawnManager spawnManager;
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

        public async void TurnChange()
        {
            switch (battleState)
            {
                case BattleState.Ready:
                    firstTurn = true;
                    battleState = BattleState.PlayerTurn;
                    Narration("Player Turn");
                    break;
                case BattleState.PlayerTurn:
                    GameManager.Instance.playerControler.ironclad.SetRelicStatus(firstTurn);
                    if (firstTurn) firstTurn = false;

                    RefreshBuff(GameManager.Instance.playerControler.playerCharacter);
                    EnemyTurn();
                    GameManager.Instance.cardManager.RemovePlayerCard();
                    break;
                case BattleState.EnemyTurn:
                    if (!GameManager.Instance.playerControler.playerCharacter.IsDead())
                    {
                        Narration("Player Turn");
                        battleState = BattleState.PlayerTurn;

                        GameManager.Instance.cardManager.ReloadPlayerCard();
                        for (int i = 0; i < enemyCharacters.Count; i++)
                        {
                            if (enemyCharacters[i].IsDead()) continue;

                            RefreshBuff(enemyCharacters[i]);
                            enemyCharacters[i].MonsterNextAction();
                        }
                    }
                    else
                    {
                        battleState = BattleState.EndBattle;
                        TurnChange();
                    }
                    break;
                case BattleState.EndBattle:
                    if (GameManager.Instance.playerControler.playerCharacter.IsDead())
                    {
                        Debug.Log("패배");
                    }
                    else
                    {
                        var ralic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;
                        if (ralic.Contains(Data.RelicType.HealFire))
                        {
                            GameManager.Instance.playerControler.ironclad.Heal(10);
                        }

                        Instantiate(rewardPop);
                        Debug.Log("승리");
                    }
                    break;
            }

            if(battleState == BattleState.PlayerTurn)
            {
                Narration("Player Turn");
                GameManager.Instance.inGameUIManager.SetTurnEndBtn(true);
                energy = maxEnergy;
                GameManager.Instance.inGameUIManager.RefreshEnergyText();
            }
            else
            {
                GameManager.Instance.inGameUIManager.SetTurnEndBtn(false);
            }
        }

        public void RefreshBuff(CharacterBase character)
        {
            for (int i = 0; i < character.listBuff.Count; i++)
            {
                character.listBuff[i].RefreshBuff();

                if (character.listBuff[i].turn == 0)
                {
                    character.RemoveBuffStat(character.listBuff[i]);
                }
            }

            character.RefreshBuffStat();
        }

        public void Narration(string text)
        {
            if (coNarration != null)
            {
                StopCoroutine(coNarration);
                coNarration = null;
            }

            coNarration = GameManager.Instance.inGameUIManager.OnNarration(text);
            StartCoroutine(coNarration);
        }

        private async UniTask EnemyTurn()
        {
            battleState = BattleState.EnemyTurn;
            Narration("Enemy Turn");

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

        public async void GetHitEffect(Transform hitCharacter)
        {
            ParticleSystem hitEffect = hitEffectPool.GetObject(hitCharacter).GetComponent<ParticleSystem>();

            hitEffect.transform.localPosition = Vector2.zero;
            await new WaitUntil(() => !hitEffect.IsAlive());

            hitEffectPool.ReturnObject(hitEffect.gameObject);
            ////////////////////////
        }
    }
}