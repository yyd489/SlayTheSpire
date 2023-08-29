using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace FrameWork
{
    public enum Buff
    {
        None = 0,
        PowerUp = 1,
        DefenceDown = 2
    }

    public abstract class CharacterBase : MonoBehaviour
    {
        protected string characterName;
        protected int maxHp;
        [SerializeField] protected int hp;
        [SerializeField] protected int shield;
        [SerializeField] protected int damage;
        [SerializeField] protected int defence;
        private int buffDamage;
        private int buffDefence;
        protected bool isMonster;
        protected bool isHold;
        protected Animator animator;

        private CharacterBase targetCharacter;

        public Vector2 charaterPos;

        public List<BuffStatus> listBuff = new List<BuffStatus>();

        [SerializeField] private GameObject hpBarPrefab;
        protected HealthBar hpBar;

        [SerializeField] protected ObjectResource objectResource;

        public abstract void Init();

        // 하위 캐릭터 오브젝트에 스크립트 넣고 죽을때 체크해야됨

#if UNITY_EDITOR
        void OnValidate()
        {
            animator = objectResource.Animator;
        }
#endif
        private void Start()
        {
            Init();
            hpBar = Instantiate(hpBarPrefab, transform.parent).transform.GetChild(0).GetComponent<HealthBar>();
            hpBar.Init(hp, maxHp, shield,transform.parent.gameObject);
        }

        public void ActiveRender(bool isActive)
        {
            if (null != objectResource)
                objectResource.ActiveRender(isActive);
        }

        // 공격, 피격등 이동
        public async Task<bool> Attack(CharacterBase target, int cardDamage, int debuffTurn, bool isAllAttack = false)
        {
            targetCharacter = target;
            ChangeState(1);
            if (!isHold)
            {
                float modifyPos = -4f;
                if (isMonster) modifyPos *= -1f;

                float attackPosX = target.transform.position.x + modifyPos;

                await transform.DOMoveX(attackPosX, 0.1f).SetEase(Ease.Linear);

                if (!isMonster)
                {
                    if (isAllAttack)
                    {
                        for (int i = 0; i < GameManager.Instance.battleManager.enemyCharacters.Count; i++)
                        {
                            targetCharacter = GameManager.Instance.battleManager.enemyCharacters[i];

                            if(debuffTurn > 0)
                            {
                                BuffStatus newBuff = new BuffStatus();
                                newBuff.InitBuff(Buff.DefenceDown, debuffTurn);
                                targetCharacter.AddBuffList(newBuff);
                            }

                            target.Hit(damage, cardDamage);
                        }
                    }
                    else
                    {
                        if (debuffTurn > 0)
                        {
                            BuffStatus newBuff = new BuffStatus();
                            newBuff.InitBuff(Buff.DefenceDown, debuffTurn);
                            targetCharacter.AddBuffList(newBuff);
                        }
                        target.Hit(damage, cardDamage);
                    }

                    ReturnPosition();
                }
            }

            return true;
        }

        public async void Hit(int attackdamage, int cardDamage)
        {
            float modifyPos = -1f;
            if (isMonster) modifyPos *= -1f;

            float knockBackPosX = transform.position.x + modifyPos;

            int targetShield = shield;
            int hitDamage = attackdamage + cardDamage + defence;

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
                if (isMonster) objectResource.ActiveRender(false);
            }
            else
            {
                await transform.DOMoveX(knockBackPosX, 0.05f).SetEase(Ease.Linear);

                ReturnPosition();
            }
        }

        protected async void ReturnPosition()
        {
            await transform.DOMoveX(charaterPos.x, 0.1f).SetEase(Ease.Linear);
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
        public void InputBuffStat()
        {
            buffDamage = 0;
            buffDefence = 0;

            for (int i = 0; i < listBuff.Count; i++)
            {
                if (listBuff[i].buff == Buff.PowerUp) buffDamage = 2;
                else if (listBuff[i].buff == Buff.DefenceDown) buffDefence = 2;
            }

            damage += buffDamage;
            defence += buffDefence;
        }

        public void AddBuffList(BuffStatus buffStatus)
        {
            listBuff.Add(buffStatus);
            InputBuffStat();
        }


        // 이벤트 트리거 함수
        public void OnPointEnter()
        {
            if (GameManager.Instance.playerControler.onDrag &&
                (GameManager.Instance.playerControler.selectCard.cardData.cardType == Data.CardType.Attack || GameManager.Instance.playerControler.selectPotion != null))

                GameManager.Instance.playerControler.TargetSet(this);
        }

        public void OnPointExit()
        {
            if (GameManager.Instance.playerControler.onDrag &&
                (GameManager.Instance.playerControler.selectCard.cardData.cardType == Data.CardType.Attack || GameManager.Instance.playerControler.selectPotion != null))
                GameManager.Instance.playerControler.TargetSet(null);
        }
    }

    public class BuffStatus
    {
        public Buff buff;
        public int turn;

        public void InitBuff(Buff initBuff, int initTurn)
        {
            buff = initBuff;
            turn = initTurn;
        }
    }
}
