using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class FungiBeast : CharacterBase
    {
        public override void Init()
        {
            name = "동물하초";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
            isHold = false;
        }
    }
}