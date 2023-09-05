using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Merchant : CharacterBase
    {
        public virtual void Init(Data.MonsterJsonData monsterStat)
        {
            name = "상인";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = false;
        }
    }
}