using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

namespace FrameWork
{
    public class Champ : CharacterBase
    {
        [SerializeField] int buffSkillTurn;
        [SerializeField] int debuffSkillTurn;


        public override void Init(Data.MonsterJsonData monsterStat)
        {
            base.Init(monsterStat);
            defence = 0;
            skillDamage = damage + 5;
            isMonster = true;
            isHoldUnit = false;
            isHaveSkillUnit = true;

            monsterAttackIcon.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 8.7f, 0));
            debuffSkillTurn = Random.Range(2, 4);
            buffSkillTurn = Random.Range(0, 3); ;
        }

        public override void MonsterNextAction()
        {
            base.MonsterNextAction();

            buffSkillTurn++;
            debuffSkillTurn++;

            if (buffSkillTurn == 5)
            {
                monsterAction = MonsterAction.BuffSkill;
                monsterAttackIcon.sprite = GameManager.Instance.battleManager.arrMonsterActionIcon[1];
                monsterDamageText.text = "";
                buffSkillTurn = Random.Range(0, 3);
            }
            else if (debuffSkillTurn == 4)
            {
                monsterAction = MonsterAction.DeBuffSkill;
                monsterAttackIcon.sprite = GameManager.Instance.battleManager.arrMonsterActionIcon[2];
                monsterDamageText.text = skillDamage.ToString();
                debuffSkillTurn = Random.Range(0, 1);
            }
            else
            {
                monsterAction = MonsterAction.Attack;
                monsterDamageText.text = damage.ToString();
                monsterAttackIcon.sprite = GameManager.Instance.battleManager.arrMonsterActionIcon[0];
            }
        }
    }
}