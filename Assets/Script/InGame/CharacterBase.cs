﻿using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
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
        [SerializeField] private int buffDamage;
        [SerializeField] private int buffDefence;
        protected bool isMonster;
        protected Animator animator;

        private CharacterBase targetCharacter;

        public Vector2 charaterPos;

        public List<BuffStatus> listBuff = new List<BuffStatus>();

        [SerializeField] private GameObject hpBarPrefab;
        protected HealthBar hpBar;

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
            if(isMonster)
            {
                name = monsterStat.monsterName;
                hp = monsterStat.hp;
                maxHp = hp;
                damage = monsterStat.monsterAttack;
            }

            hpBar = Instantiate(hpBarPrefab, transform.parent).transform.GetChild(0).GetComponent<HealthBar>();
            hpBar.Init(hp, maxHp, shield, transform.parent.gameObject);
        }

        public void ActiveRender(bool isActive)
        {
            if (null != objectResource)
                objectResource.ActiveRender(isActive);
        }

        // 공격, 피격등 이동
        public async Task<bool> Attack(CharacterBase target, int cardDamage, int debuffTurn, bool isAllAttack = false)
        {
            if (!isMonster)
            {
                if (isAllAttack)
                {
                    for (int i = 0; i < GameManager.Instance.battleManager.enemyCharacters.Count; i++)
                    {
                        targetCharacter = GameManager.Instance.battleManager.enemyCharacters[i];

                        if (debuffTurn > 0)
                            targetCharacter.AddBuffList(Buff.DefenceDown, debuffTurn);

                        targetCharacter.Hit(damage + buffDamage, cardDamage);
                    }
                }
                else
                {
                    ChangeState(1);
                    targetCharacter = target;
                    float modifyPos = -4f;
                    if (isMonster) modifyPos *= -1f;

                    float attackPosX = target.transform.position.x + modifyPos;

                    await transform.DOMoveX(attackPosX, 0.1f).SetEase(Ease.Linear);

                    if (debuffTurn > 0)
                        targetCharacter.AddBuffList(Buff.DefenceDown, debuffTurn);

                    target.Hit(damage + buffDamage, cardDamage);


                    ReturnPosition();
                }
            }
            else
            {
                ChangeState(1);
                targetCharacter = target;
                float modifyPos = -4f;
                if (isMonster) modifyPos *= -1f;

                float attackPosX = target.transform.position.x + modifyPos;

                await transform.DOMoveX(attackPosX, 0.1f).SetEase(Ease.Linear);
            }

            return true;
        }

        public async void Hit(int attackdamage, int cardDamage = 0)
        {
            float modifyPos = -1f;
            if (isMonster) modifyPos *= -1f;

            float knockBackPosX = transform.position.x + modifyPos;

            int targetShield = shield;
            int hitDamage = attackdamage + cardDamage + defence + buffDefence;

            if (targetShield > 0 )
            {
                if (targetShield > hitDamage)
                    shield -= hitDamage;
                else
                {
                    hitDamage -= shield;
                    shield = 0;
                }
            }

            hp -= hitDamage;

            hpBar.SetHealthGauge(hp, maxHp, shield);

            if (IsDead())
            {
                List<CharacterBase> enemys = GameManager.Instance.battleManager.enemyCharacters;
                for (int i = 0; i < enemys.Count; i++)
                {
                    if (!enemys[i].IsDead()) return;
                }
                GameManager.Instance.battleManager.battleState = BattleState.EndBattle;
                GameManager.Instance.battleManager.TurnChange();

                if (isMonster)
                {
                    transform.parent.gameObject.SetActive(false);
                    objectResource.ActiveRender(false);
                }
            }
            else
            {
                await transform.DOMoveX(knockBackPosX, 0.05f).SetEase(Ease.Linear);

                ReturnPosition();
            }
        }

        // 애니메이션 이벤트용 공격 함수
        public void TargetHit() => targetCharacter.Hit(damage + buffDamage);

        protected async void ReturnPosition()
        {
            await transform.DOLocalMoveX(charaterPos.x, 0.1f).SetEase(Ease.Linear);
            ChangeState(0);
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
        public void RefreshBuffStat()
        {
            buffDamage = 0;
            buffDefence = 0;

            for (int i = 0; i < listBuff.Count; i++)
            {
                if (listBuff[i].buff == Buff.PowerUp) buffDamage = 2;
                else if (listBuff[i].buff == Buff.DefenceDown) buffDefence = 2;
            }
        }

        public void AddBuffList(Buff buff, int buffTurn)
        {
            BuffStatus newBuff = hpBar.buffIconPool.GetObject(hpBar.iconParent).GetComponent<BuffStatus>();
            newBuff.InitBuff(buff, buffTurn);

            listBuff.Add(newBuff);
            RefreshBuffStat();
        }

        public void RemoveBuffStat(BuffStatus buffStatus)
        {
            hpBar.buffIconPool.ReturnObject(buffStatus.gameObject);
            listBuff.Remove(buffStatus);
        }


        // 이벤트 트리거 함수
        public void OnPointEnter()
        {
            if (GameManager.Instance.playerControler.onDrag &&
                (GameManager.Instance.playerControler.selectCard.cardData.cardType == Data.CardType.Attack && GameManager.Instance.playerControler.selectCard.cardData.cardName != "천둥" 
                || GameManager.Instance.playerControler.selectPotion != null))

                GameManager.Instance.playerControler.TargetSet(this);
        }

        public void OnPointExit()
        {
            if (GameManager.Instance.playerControler.onDrag &&
                (GameManager.Instance.playerControler.selectCard.cardData.cardType == Data.CardType.Attack && GameManager.Instance.playerControler.selectCard.cardData.cardName != "천둥"
                || GameManager.Instance.playerControler.selectPotion != null))
                GameManager.Instance.playerControler.TargetSet(null);
        }
    }
}
