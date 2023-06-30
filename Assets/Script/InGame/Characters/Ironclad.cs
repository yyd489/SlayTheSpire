using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ironclad : CharacterBase
{
    
    public override void Init() 
    {
        name = "Ironclad";
        healthPoint = 100f;
        maxHealthPoint = healthPoint;
        attackDamage = 0f;
        defence = 0f;
    }
    public override void Attack() { }
    public override void Hit() { }
}
