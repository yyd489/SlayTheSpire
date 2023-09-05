using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Champ : CharacterBase
    {
        public override void Init(Data.MonsterJsonData monsterStat)
        {
            defence = 0;
            isMonster = true;

            base.Init(monsterStat);
        }
    }
}