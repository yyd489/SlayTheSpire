using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class Ironclad : CharacterBase
    {
        public virtual void Init(Data.CharacterCollect characterStat)
        {
            base.Init(characterStat);

            damage = defence = 0;
            isMonster = false;
        }

        public void SetRelicStatus(bool first)
        {
            if (first)
            {
                var dicRelic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;

                if (dicRelic.Contains(Data.RelicType.Accursed))
                    damage += 2;
                else if (dicRelic.Contains(Data.RelicType.Void))
                    defence += 10;
            }
            else
            {
                damage = defence = 0;
            }
        }

        public void Heal(int heal)
        {
            hp += heal;

            if (hp > maxHp) hp = maxHp;

            hpBar.SetHealthGauge(hp, maxHp, shield);
        }

        public void RelicHealthUp()
        {
            maxHp += 10;
            hp += 10;

            if (hp > maxHp) hp = maxHp;

            hpBar.SetHealthGauge(hp, maxHp, shield);
        }
    }
}