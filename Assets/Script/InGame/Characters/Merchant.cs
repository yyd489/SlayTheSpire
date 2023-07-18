using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Merchant : CharacterBase
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            name = "상인";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
        }

        public override void Attack(CharacterBase target)
        {
            base.Attack(target);
        }
    }
}