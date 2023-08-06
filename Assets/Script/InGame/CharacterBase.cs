using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace FrameWork
{
    public abstract class CharacterBase : MonoBehaviour
    {
        protected string characterName;
        protected int maxHp;
        protected int hp;
        protected int shield;
        protected int damage;
        protected int defence;
        protected bool isMonster;
        protected bool isHold;
        protected Animator animator;

        private CharacterBase targetCharacter;

        private Vector2 charaterPos;

        [SerializeField] protected ObjectResource objectResource;

        public abstract void Init();

#if UNITY_EDITOR
        void OnValidate()
        {
            animator = objectResource.Animator;
            charaterPos = transform.position;
        }
#endif
        public void ActiveRender(bool isActive)
        {
            if (null != objectResource)
                objectResource.ActiveRender(isActive);
        }

        public void InitHp(int health)
        {
            hp = health;
        }

        public async Task<bool> Attack(CharacterBase target, int cardDamage)
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
                    TargetHit(cardDamage);
                    ReturnPosition();
                }
            }

            return true;
        }

        protected async void TargetHit(int cardDamage)
        {
            float modifyPos = -1f;
            if (targetCharacter.isMonster) modifyPos *= -1f;

            float knockBackPosX = targetCharacter.transform.position.x + modifyPos;

            await targetCharacter.transform.DOMoveX(knockBackPosX, 0.05f).SetEase(Ease.Linear);
            targetCharacter.hp -= damage + cardDamage;

            if (IsDead())
            {
                if (targetCharacter.isMonster) targetCharacter.objectResource.ActiveRender(false);
                Debug.Log("사망");
            }
            targetCharacter.ReturnPosition();
        }

        protected async void ReturnPosition()
        {
            await transform.DOMoveX(charaterPos.x, 0.1f).SetEase(Ease.Linear);
            ChangeState(0);
        }

        protected void SetPosition(Vector3 characterPos)
        {
            if (isMonster) transform.localScale = new Vector3(-1f, 1f, 1f);
            transform.position = characterPos;
        }

        private bool IsDead()
        {
            return hp <= 0;
        }

        protected void ChangeState(int animIndex = 0)
        {
            if(animator == null) animator = GetComponent<Animator>();
            
            animator.SetInteger("state", animIndex);
        }

        public void OnPointEnter()
        {
            GameManager.playerControler.targetCharacter = this;
        }

        public void OnPointExit()
        {
            GameManager.playerControler.targetCharacter = null;
        }
    }
}
