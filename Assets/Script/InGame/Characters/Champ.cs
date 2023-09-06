using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FrameWork
{
    public class Champ : CharacterBase
    {
        [SerializeField] TextMeshProUGUI monsterDamageText;
        public override void Init(Data.MonsterJsonData monsterStat)
        {
            base.Init(monsterStat);
            defence = 0;
            isMonster = true;
            isHoldUnit = false;

            monsterDamageText.text = damage.ToString();
        }

        //public override void A
    }
}