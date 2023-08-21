using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Ironclad : CharacterBase
    {
        public override void Init()
        {
            name = "아이언 클래드";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = false;
            isHold = false;

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = 0.25f;
            pos.y = 0.5f;
            transform.parent.position = Camera.main.ViewportToWorldPoint(pos);
            charaterPos = transform.parent.position;
        }
    }
}