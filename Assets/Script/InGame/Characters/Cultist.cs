using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading;

namespace FrameWork
{
    public class Cultist : CharacterBase
    {
        private void Start()
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = 0.8f;
            Debug.Log(gameObject.name + " : " + pos.x);
            transform.position = Camera.main.ViewportToWorldPoint(pos);
            charaterPos = transform.position;
            Init();
        }

        public override void Init()
        {
            name = "광신자";
            hp = 100;
            maxHp = hp;
            damage = 0;
            defence = 0;
            isMonster = true;
            isHold = false;
        }
    }
}