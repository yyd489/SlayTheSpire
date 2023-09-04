﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class JawWorm : CharacterBase
    {
        public override void Init()
        {
            name = "턱벌레";
            hp = 1;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
            isHold = false;
        }
    }
}