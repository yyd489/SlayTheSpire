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
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += heal;

            hp = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp;

            maxHp = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp;

            if (hp > maxHp) hp = maxHp;

          

            GameManager.Instance.ingameTopUI.nowHpText.text = "" + hp + "/" + maxHp;

         

            hpBar.SetHealthGauge(hp, maxHp, shield);

        }

        public void RelicHealthUp()
        {
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += 10;

            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp += 10;

            hp = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp;

            maxHp = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp;

            if (hp > maxHp) hp = maxHp;

       

            GameManager.Instance.ingameTopUI.nowHpText.text = "" + hp + "/" + maxHp;

            hpBar.SetHealthGauge(hp, maxHp, shield);
        }

        public void ResetShield()
        {
            shield = 0;
            hpBar.SetHealthGauge(hp, maxHp, shield);
        }

       

    }
}