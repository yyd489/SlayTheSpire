using UnityEngine;
using DG.Tweening;

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

        [SerializeField] protected ObjectResource objectResource;

        public abstract void Init();

#if UNITY_EDITOR
        void OnValidate()
        {
            animator = objectResource.Animator;
        }
#endif
        public void ActiveRender(bool isActive)
        {
            if (null != objectResource)
                objectResource.ActiveRender(isActive);
        }

        public void Attack(Vector2 targetPos)
        {
            float modifyPos = 0.1f;
            if (isMonster) modifyPos = -0.1f;

            Vector2 attackPos = new Vector2(targetPos.x + modifyPos, transform.position.y);
            ChangeState(1);

            transform.DOPunchPosition(attackPos, 2f);
        }

        public void InitHp(int health)
        {
            hp = health;
        }

        public void Hit(int hitDamage)
        {
            float modifyPos = -0.1f;
            if (isMonster) modifyPos = 0.1f;

            Vector3 knockBackPos = new Vector3(transform.position.x + modifyPos, transform.position.y);
            transform.DOPunchPosition(knockBackPos, 0.1f);

            hp -= hitDamage;

            if (IsDead())
            {
                if (isMonster) gameObject.SetActive(false);
                Debug.Log("사망");
            }
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
