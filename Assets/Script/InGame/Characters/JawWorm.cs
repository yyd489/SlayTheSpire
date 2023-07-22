using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class JawWorm : CharacterBase
    {
        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            name = "턱벌레";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
            isHold = false;
        }
    }
}