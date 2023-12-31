﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
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
        private int maxEnergy = 3;
        public int energy;

        [SerializeField] public List<CharacterBase> enemyCharacters = new List<CharacterBase>();

        public Sprite[] arrBuffIcon;
        public Sprite[] arrMonsterActionIcon;

        [SerializeField] private SpawnManager spawnManager;

        public ObjectPool hitEffectPool;
        [SerializeField] private GameObject rewardPop;

        public MapField stage;

        public void Init()
        {
            energy = maxEnergy;
            battleState = BattleState.Ready;
            TurnChange();
            //spawnManager.Init();            
        }

        public async void TurnChange()
        {
            if (GameManager.Instance.playerControler.onDrag) return;

            switch (battleState)
            {
                case BattleState.Ready:
                    GameManager.Instance.playerControler.ironclad.SetRelicStatus();
                    battleState = BattleState.PlayerTurn;
                    GameManager.Instance.inGameUIManager.Narration("Player Turn");
                    break;

                case BattleState.PlayerTurn:

                    RefreshBuff(GameManager.Instance.playerControler.playerCharacter);
                    GameManager.Instance.cardManager.RemovePlayerCard();

                    if (enemyCharacters.Count == 0 || GameManager.Instance.playerControler.ironclad.IsDead())
                    {
                        EndBattle();
                    }
                    else
                        EnemyTurn();
                    break;

                case BattleState.EnemyTurn:
                    if (!GameManager.Instance.playerControler.playerCharacter.IsDead())
                    {
                        GameManager.Instance.inGameUIManager.Narration("Player Turn");
                        battleState = BattleState.PlayerTurn;
                        GameManager.Instance.playerControler.ironclad.ResetShield();

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
                        EndBattle();
                    }
                    break;
                case BattleState.EndBattle:
                    break;
            }

            if (battleState == BattleState.PlayerTurn)
            {
                GameManager.Instance.inGameUIManager.Narration("Player Turn");
                GameManager.Instance.inGameUIManager.SetTurnEndBtn(true);
                energy = maxEnergy;
                GameManager.Instance.inGameUIManager.RefreshEnergyText(0);
            }
            else
            {
                GameManager.Instance.inGameUIManager.SetTurnEndBtn(false);
            }
        }

        public void RefreshBuff(CharacterBase character)
        {
            int indexCount = character.listBuff.Count;
            int index = 0;
            for (int i = 0; i < indexCount; i++)
            {
                character.listBuff[index].RefreshBuff();

                if (character.listBuff[index].turn == 0)
                {
                    character.RemoveBuffStat(character.listBuff[index]);
                }
                else index++;
            }

            character.RefreshBuffStat();
        }

        private async UniTask EnemyTurn()
        {
            battleState = BattleState.EnemyTurn;
            GameManager.Instance.inGameUIManager.Narration("Enemy Turn");

            TimeSpan delayTime = TimeSpan.FromSeconds(1);
            bool isLosePlayer = false;
            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                await UniTask.Delay(delayTime);
                await enemyCharacters[i].Attack(GameManager.Instance.playerControler.playerCharacter, 0, 0);
                await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

                if (GameManager.Instance.playerControler.playerCharacter.IsDead())
                    return;
            }

            if (!isLosePlayer)
                await UniTask.Delay(delayTime);

            TurnChange();
        }

        private async void EndBattle()
        {
            battleState = BattleState.EndBattle;

            if (GameManager.Instance.playerControler.ironclad.IsDead())//죽었을 때
            {
                var GameObject = Instantiate(GameManager.Instance.initilizer.lostPop);//.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
                await GameObject.GetComponent<CanvasGroup>().DOFade(0.8f, 0.5f);
                GameObject.Find("NextButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameManager.Instance.LoadMainTitle());
                GameObject.Find("NextButton").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => GameManager.Instance.soundManager.effectPlaySound(2));
            }
            else//이겻을 때
            {
                GameManager.Instance.playerControler.ironclad.ResetShield();
                GameManager.Instance.cardManager.ResetCardDecks();
                if (MapManager.fieldInfo != MapField.Boss)
                {
                    Instantiate(rewardPop);

                    var ralic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;
                    if (ralic.Contains(Data.RelicType.HealFire))
                    {
                        GameManager.Instance.playerControler.ironclad.Heal(10);
                    }
                }
                else
                {
                    Debug.Log("보스끝");
                    AsyncUIregister.InstansUI("Assets/Prefabs/UI/WinPanel.prefab");

                    await UniTask.WaitUntil(() => GameObject.Find("WinPanel(Clone)") != null);

                    var winPanel = GameObject.Find("WinPanel(Clone)").transform.Find("NextButton").GetComponent<UnityEngine.UI.Button>();
                    winPanel.onClick.AddListener(() => GameManager.Instance.soundManager.effectPlaySound(2));
                    winPanel.onClick.AddListener(() => GameManager.Instance.LoadMainTitle());
                    await GameObject.Find("WinPanel(Clone)").GetComponent<CanvasGroup>().DOFade(1, 1f);
                }
            }
        }

        public async UniTask GetHitEffect(CharacterBase hitCharater)
        {
            ParticleSystem hitEffect = hitEffectPool.GetObject(hitEffectPool.transform).GetComponent<ParticleSystem>();

            hitEffect.transform.position = hitCharater.centerPos.position;
            await UniTask.Delay(TimeSpan.FromSeconds(hitEffect.duration));
            if(!GameManager.Instance.playerControler.ironclad.IsDead())
                hitEffectPool.ReturnObject(hitEffect.gameObject);
        }

        public bool IsBattleStage()
        {
            if (stage == MapField.Monster || stage == MapField.EliteMonster || stage == MapField.Boss)
            {
                return true;
            }

            return false;
        }
    }
}