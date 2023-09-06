using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;

namespace FrameWork
{
    public class Sentry : CharacterBase
    {
        [SerializeField] TextMeshProUGUI monsterDamageText;
        int turn = 4;

        public override void Init(Data.MonsterJsonData monsterStat)
        {
            base.Init(monsterStat);
            defence = 0;
            isMonster = true;
            isHoldUnit = true;

            monsterDamageText.text = damage.ToString();
        }

        public override Task<bool> Attack(CharacterBase target, int cardDamage, int debuffTurn, bool isAllAttack = false)
        {
            if (turn == 3) monsterAction = MonsterAction.DeBuffSkill;
            else monsterAction = MonsterAction.Attack;

            turn--;
            if (turn == 0) turn = Random.Range(3, 4);

            return base.Attack(target, cardDamage, debuffTurn, isAllAttack);
        }
    }
}