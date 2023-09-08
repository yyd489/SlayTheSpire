using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FrameWork
{
    public class FungiBeast : CharacterBase
    {
        public override void Init(Data.MonsterJsonData monsterStat)
        {
            base.Init(monsterStat);
            defence = 0;
            isMonster = true;
            isHoldUnit = false;
            isHaveSkillUnit = false;

            monsterAttackIcon.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(-0.3f, 3.8f, 0));
            monsterDamageText.text = damage.ToString();
            monsterAction = MonsterAction.Attack;

        }

        public override void MonsterNextAction()
        {
            base.MonsterNextAction();

            monsterAttackIcon.sprite = GameManager.Instance.battleManager.arrMonsterActionIcon[0];
        }
    }
}