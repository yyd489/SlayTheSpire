using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace FrameWork
{
    public class Sentry : CharacterBase
    {
        int turn;

        public override void Init(Data.MonsterJsonData monsterStat)
        {
            base.Init(monsterStat);
            skillDamage = 0;
            defence = 0;
            isMonster = true;
            isHoldUnit = true;
            isHaveSkillUnit = true;

            monsterAttackIcon.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.1f, 5.7f, 0));
            turn = Random.Range(3, 4);
            monsterDamageText.text = damage.ToString();
        }

        public override void MonsterNextAction()
        {
            base.MonsterNextAction();

            if (turn == 3)
            {
                monsterAction = MonsterAction.DeBuffSkill;
                monsterDamageText.text = "";
                monsterAttackIcon.sprite = GameManager.Instance.battleManager.arrMonsterActionIcon[2];
            }
            else
            {
                monsterAction = MonsterAction.Attack;
                monsterDamageText.text = damage.ToString();
                monsterAttackIcon.sprite = GameManager.Instance.battleManager.arrMonsterActionIcon[0];
            }

            turn--;
            if (turn == 0) turn = Random.Range(3, 4);
        }
    }
}