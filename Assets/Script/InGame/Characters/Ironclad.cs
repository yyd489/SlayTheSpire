using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class Ironclad : CharacterBase
    {
        public virtual void Init(Data.MonsterJsonData monsterStat)
        {            
            var characterStat = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect;
            name = characterStat.name;
            maxHp = characterStat.hp;
            hp = characterStat.maxHp;
            damage = defence = 0;
            isMonster = false;

            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            pos.x = 0.25f;
            pos.y = 0.57f;
            transform.parent.position = Camera.main.ViewportToWorldPoint(pos);
            charaterPos = transform.localPosition;

            base.Init(monsterStat);
            //SetRelicStatus(true);
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