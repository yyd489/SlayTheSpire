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

        public void SetRelicStatus()
        {
            var listRelic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;

            if (listRelic.Contains(Data.RelicType.Accursed))
            {
                AddBuffList(Buff.PowerUp, 1);
            }

            if (listRelic.Contains(Data.RelicType.Void))
            {
                shield = 10;
                hpBar.SetHealthGauge(hp, maxHp, shield);
            }
        }

        public void Heal(int heal)
        {
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += heal;

            hp+=10;
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp = hp;

            maxHp = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp;

            if (hp > maxHp) hp = maxHp;

          

            GameManager.Instance.ingameTopUI.nowHpText.text = "" + hp + "/" + maxHp;

         

            hpBar.SetHealthGauge(hp, maxHp, shield);

        }

        public void RelicHealthUp()
        {
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += 10;

            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp += 10;

            hp += 10;
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp = hp;
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