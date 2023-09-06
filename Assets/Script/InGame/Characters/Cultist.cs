using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FrameWork
{
    public class Cultist : CharacterBase
    {
        [SerializeField] TextMeshProUGUI monsterDamageText;
        public override void Init(Data.MonsterJsonData monsterStat)
        {
            base.Init(monsterStat);
            defence = 0;
            isMonster = true;
            isHoldUnit = false;

            monsterDamageText.text = damage.ToString();
            monsterAction = MonsterAction.Attack;
        }
    }
}