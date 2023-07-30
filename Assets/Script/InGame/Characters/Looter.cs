﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Looter : CharacterBase
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            name = "도적";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
            isHold = false;
        }
    }
}