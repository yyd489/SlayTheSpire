using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Sentry : CharacterBase
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            name = "보초기";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
        }

        public override void Attack(CharacterBase target)
        {
        }
    }
}