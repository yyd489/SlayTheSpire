using UnityEngine;
using DG.Tweening;
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
        protected Animator animator;
        [SerializeField] private float moveSpeed;

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

        public virtual async void Attack(CharacterBase target)
        {
            float modifyPos = -2f;
            if (isMonster) modifyPos *= -1f;

            float attackPosX = target.transform.position.x + modifyPos;

            await transform.DOMoveX(attackPosX, 0.1f).SetEase(Ease.Linear);

            ChangeState(1);
            target.Hit(damage);
        }

        public async void Hit(int hitDamage)
        {
            float modifyPos = -1f;
            if (isMonster) modifyPos *= -1f;

            float knockBackPosX = transform.position.x + modifyPos;

            await transform.DOMoveX(knockBackPosX, 0.05f).SetEase(Ease.Linear);
            hp -= hitDamage;
            ReturnPosition();

            if (IsDead())
            {
                if (isMonster) objectResource.ActiveRender(false);
                Debug.Log("사망");
            }
        }

        public async void ReturnPosition()
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

        private void ChangeState(int animIndex = 0)
        {
            if(animator == null) animator = GetComponent<Animator>();
            
            animator.SetInteger("state", animIndex);
        }
    }
}
