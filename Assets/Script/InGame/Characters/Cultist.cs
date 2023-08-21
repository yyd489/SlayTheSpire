using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Cultist : CharacterBase
    {
        public override void Init()
        {
            name = "광신자";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
            isHold = false;
        }
    }
}