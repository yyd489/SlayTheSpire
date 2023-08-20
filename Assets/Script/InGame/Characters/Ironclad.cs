using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Ironclad : CharacterBase
    {
        private void Start()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = 0.25f;
            transform.position = Camera.main.ViewportToWorldPoint(pos);
            Debug.Log(gameObject.name + " : " + transform.position.x);
            charaterPos = transform.position;
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
            isHold = false;
        }
    }
}