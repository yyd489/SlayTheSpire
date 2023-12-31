﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace FrameWork
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected string characterName;
        protected int maxHp;
        [SerializeField] protected int hp;
        [SerializeField] protected int shield;
        [SerializeField] protected int damage;
        [SerializeField] protected int defence;
        [SerializeField] protected int skillDamage;
        [SerializeField] private int buffDamage;
        [SerializeField] private int buffDefence;
        protected bool isMonster;
        protected bool isHoldUnit;
        public bool isHaveSkillUnit;

        protected Animator animator;
        private CharacterBase targetCharacter;

        public Vector2 charaterPos;

        public List<BuffStatus> listBuff = new List<BuffStatus>();

        [SerializeField] private GameObject hpBarPrefab;
        protected HealthBar hpBar;

        [SerializeField] protected Image monsterAttackIcon;
        [SerializeField] protected TextMeshProUGUI monsterDamageText;
        protected MonsterAction monsterAction = MonsterAction.Attack;

        public Transform centerPos;
        [SerializeField] protected ObjectResource objectResource;

        // 하위 캐릭터 오브젝트에 스크립트 넣고 죽을때 체크해야됨

#if UNITY_EDITOR
        void OnValidate()
        {
            animator = objectResource.Animator;
        }
#endif
        public virtual void Init(Data.MonsterJsonData monsterStat)
        {
            name = monsterStat.monsterName;
            hp = monsterStat.hp;
            maxHp = hp;
            damage = monsterStat.monsterAttack;
            hpBar = Instantiate(hpBarPrefab, transform.parent).transform.GetChild(0).GetComponent<HealthBar>();
            hpBar.Init(hp, maxHp, shield, transform.parent.gameObject);

            monsterAttackIcon.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0f, 4.5f, 0));
            MonsterNextAction();
        }

        public virtual void Init(Data.CharacterCollect characterStat)
        {
            name = characterStat.name;
            hp = characterStat.hp;
            maxHp = characterStat.maxHp;

            hpBar = Instantiate(hpBarPrefab, transform.parent).transform.GetChild(0).GetComponent<HealthBar>();
            hpBar.Init(hp, maxHp, shield, transform.parent.gameObject);
        }

        public void ActiveRender(bool isActive)
        {
            if (null != objectResource)
                objectResource.ActiveRender(isActive);
        }

        // 공격, 피격등 이동
        public async UniTask Attack(CharacterBase target, int cardDamage, int debuffTurn, bool isAllAttack = false)
        {
            if (!isMonster)
            {
                if (isAllAttack)
                {
                    GameManager.Instance.soundManager.playBattleEffectSound(0);

                    List<CharacterBase> enemys = GameManager.Instance.battleManager.enemyCharacters;

                    for (int i = enemys.Count - 1; i >= 0; i--)
                    {
                        targetCharacter = enemys[i];

                        if (debuffTurn > 0)
                            targetCharacter.AddBuffList(Buff.DefenceDown, debuffTurn);

                        targetCharacter.Hit(damage + buffDamage, cardDamage);
                    }

                    await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
                    GameManager.Instance.soundManager.playBattleEffectSound(2);
                }
                else
                {
                    ChangeState(1);
                    targetCharacter = target;
                    float modifyPos = -4f;

                    float attackPosX = target.transform.position.x + modifyPos;

                    await transform.DOMoveX(attackPosX, 0.1f).SetEase(Ease.Linear);

                    target.Hit(damage + buffDamage, cardDamage);
                    GameManager.Instance.soundManager.playBattleEffectSound(0);

                    ReturnPosition();

                    if (debuffTurn > 0)
                    {
                        targetCharacter.AddBuffList(Buff.DefenceDown, debuffTurn);

                        await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
                        GameManager.Instance.soundManager.playBattleEffectSound(2);
                    }
                }
            }
            else
            {
                monsterAttackIcon.gameObject.SetActive(false);
                ChangeState(1);
                targetCharacter = target;

                if (!isHoldUnit && monsterAction != MonsterAction.BuffSkill)
                {
                    float modifyPos = 4f;

                    float attackPosX = target.transform.position.x + modifyPos;

                    await transform.DOMoveX(attackPosX, 0.1f).SetEase(Ease.Linear);
                }
            }
        }

        public async void Hit(int attackdamage, int cardDamage = 0)
        {
            float modifyPos = -1f;
            if (isMonster) modifyPos *= -1f;

            float knockBackPosX = transform.position.x + modifyPos;

            int hitDamage = attackdamage + cardDamage + defence + buffDefence;

            if (attackdamage + cardDamage > 0)
                GameManager.Instance.battleManager.GetHitEffect(this);

            if (shield > hitDamage)
            {
                shield -= hitDamage;
            }
            else
            {
                hitDamage -= shield;
                shield = 0;

                hp -= hitDamage;
            }

            if (hp < 0) hp = 0;
           
            hpBar.SetHealthGauge(hp, maxHp, shield);

            if(!isMonster)
            GameManager.Instance.ingameTopUI.nowHpText.text ="" + hp + "/" + maxHp;
            
            if (IsDead())
            {
                if (isMonster)
                {
                    List<CharacterBase> enemys = GameManager.Instance.battleManager.enemyCharacters;
                    enemys.Remove(this);
                    if (enemys.Count == 0)
                    {
                        //GameManager.Instance.playerControler.onDrag = false;
                        GameManager.Instance.battleManager.TurnChange();
                    }
                    Destroy(transform.parent.gameObject);

                    //objectResource.ActiveRender(false);
                    //transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    GameManager.Instance.battleManager.TurnChange();
                }
            }
            else
            {
                await transform.DOMoveX(knockBackPosX, 0.05f).SetEase(Ease.Linear);

                ReturnPosition();
            }
        }

        // 애니메이션 이벤트용 공격 함수
        public async UniTask TargetHit()
        {
            if (monsterAction == MonsterAction.Attack)
            {
                targetCharacter.Hit(damage + buffDamage);
                GameManager.Instance.soundManager.playBattleEffectSound(0);
            }
            else if (monsterAction == MonsterAction.DeBuffSkill)
            {
                targetCharacter.Hit(skillDamage);
                targetCharacter.AddBuffList(Buff.PowerDown, 2);
                if(skillDamage > 0)
                {
                    GameManager.Instance.soundManager.playBattleEffectSound(0);
                    await UniTask.Delay(System.TimeSpan.FromSeconds(0.5f));
                }
                GameManager.Instance.soundManager.playBattleEffectSound(2);
            }
            else if (monsterAction == MonsterAction.BuffSkill)
            {
                AddBuffList(Buff.PowerUp, 2);
                GameManager.Instance.soundManager.playBattleEffectSound(1);
            }
        }

        protected async void ReturnPosition()
        {
            await transform.DOLocalMoveX(charaterPos.x, 0.1f).SetEase(Ease.Linear);
            ChangeState(0);
        }

        public virtual void MonsterNextAction()
        {
            monsterAttackIcon.gameObject.SetActive(true);
        }

        // 스텟 관련
        public bool IsDead()
        {
            return hp <= 0;
        }

        private void ChangeState(int animIndex = 0)
        {
            if(animator == null) animator = objectResource.Animator;
            
            animator.SetInteger("state", animIndex);
        }

        public void SetShield(int getShield)
        {
            shield += getShield;
            hpBar.SetHealthGauge(hp, maxHp, shield);
        }

        // 버프
        public void AddBuffList(Buff buff, int buffTurn)
        {
            BuffStatus newBuff = hpBar.buffIconPool.GetObject(hpBar.iconParent).GetComponent<BuffStatus>();
            newBuff.InitBuff(buff, buffTurn);

            listBuff.Add(newBuff);
            RefreshBuffStat();
        }

        public void RefreshBuffStat()
        {
            buffDamage = 0;
            buffDefence = 0;

            for (int i = 0; i < listBuff.Count; i++)
            {
                if (listBuff[i].buff == Buff.PowerUp) buffDamage = 2;
                else if (listBuff[i].buff == Buff.DefenceDown) buffDefence = 2;
                else if (listBuff[i].buff == Buff.PowerDown) buffDamage = -2;
            }
        }

        public void RemoveBuffStat(BuffStatus buffStatus)
        {
            hpBar.buffIconPool.ReturnObject(buffStatus.gameObject);
            listBuff.Remove(buffStatus);
        }


        // 이벤트 트리거 함수
        public void OnPointEnter()
        {
            if (GameManager.Instance.playerControler.onDrag && CheckPlayerSelectItem())
            {
                GameManager.Instance.playerControler.TargetSet(this);
            }
        }

        public void OnPointExit()
        {
            if (GameManager.Instance.playerControler.onDrag && CheckPlayerSelectItem())
                GameManager.Instance.playerControler.TargetSet(null);
        }

        private bool CheckPlayerSelectItem()
        {
            return (GameManager.Instance.playerControler.selectCard != null
                && GameManager.Instance.playerControler.selectCard.cardData.cardType == Data.CardType.Attack
                    && GameManager.Instance.playerControler.selectCard.cardData.cardName != "천둥")
                    || GameManager.Instance.playerControler.selectPotion != null;
        }
    }
}
