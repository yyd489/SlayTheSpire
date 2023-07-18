﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Ironclad : CharacterBase
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            name = "아이언 클래드";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = false;
        }

        public override void Attack(CharacterBase target)
        {
            base.Attack(target);
        }
    }
}