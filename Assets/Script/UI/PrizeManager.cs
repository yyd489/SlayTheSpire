using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace FrameWork
{
    using FrameWork.Data;
    public class PrizeManager : MonoBehaviour
    {

        public TextMeshProUGUI gold;
        public int rewardGold;

        public void Init()
        {
            rewardGold = Random.Range(17, 22);
            gold.text = rewardGold.ToString();
        }

        public void ClickRewardButton()
        {
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.gold += rewardGold;
            GameManager.Instance.ingameUI.ChangeState();
        }

    }
}
