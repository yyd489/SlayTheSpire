using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
namespace FrameWork
{
    using FrameWork.Data;
    public class PrizeManager : MonoBehaviour
    {

        public TextMeshProUGUI gold;
        public int rewardGold;
        public Image rewardImage;
        public TextMeshProUGUI rewardText;
        public Button addGoldButton;
        public Button rewardButton;
        public Sprite cardSprite;
        

        public void Init()
        {
            rewardGold = Random.Range(17, 22);
            gold.text = rewardGold.ToString();
            addGoldButton.onClick.AddListener(() => ClickGoldButton());

        }

        public void ClickGoldButton()
        {
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.gold += rewardGold;
            GameManager.Instance.ingameUI.ChangeState();
            Destroy(addGoldButton.gameObject);
        }

        public void ClickRewardButton()
        {
            int randomReward = Random.Range(0, 2);

            switch(randomReward)
            {
                case 0://카드
                    rewardImage.sprite = cardSprite;
                    rewardText.text = "덱으로 카드 추가";
                   
                    break;

                case 1:

                    var itemData = GameManager.Instance.dataManager.data.itemData;
                    var potionList = itemData.GetPotionData();
                    int randomPotionIndex = Random.Range(0, potionList.Count);
                    rewardImage.sprite = GameManager.Instance.ingameUI.listPotionPrefab[randomPotionIndex].GetComponent<Sprite>();
                    rewardText.text = potionList[randomPotionIndex].effectGuide;

                    break;

            }

        }

        public void AddPotion(int radomPotionIndex)
        {
            var havePotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHavePotion;
            
            if(havePotion.Count >=3)
            {
                Debug.Log("불가");
            }
            else
            {
                GameManager.Instance.ingameUI.AddPotion(radomPotionIndex);
            }
        }

     }

    
}
