using UnityEngine;

public enum CharacterState
{ 
    idle = 0,
    attack = 1,
    hit = 2,
    dead = 3
}

public abstract class CharacterBase : MonoBehaviour
{
    protected string characterName;
    protected float maxHealthPoint;
    protected float healthPoint;
    protected float shield;
    protected float attackDamage;
    protected float defence;
    protected CharacterBase targetCharacter;

    private CharacterState state;

    public abstract void Init();
    public abstract void Attack();
    public abstract void Hit();

    private void ChangeState()
    {

    }
}
