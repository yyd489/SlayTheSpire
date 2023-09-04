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
        public Button cardNextButton;
        public Sprite cardSprite;
        public Button nextButton;
        public GameObject cardPrefab;
        public Transform selectCardPanel;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            rewardGold = Random.Range(17, 22);
            gold.text = rewardGold.ToString()+"골드";
            addGoldButton.onClick.AddListener(() => ClickGoldButton());
            nextButton.onClick.AddListener(() => GameManager.Instance.stageManager.ClearStage(this.transform.parent.gameObject));
            ClickRewardButton();

        }

        public void ClickGoldButton()
        {
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.gold += rewardGold;
            GameManager.Instance.ingameTopUI.ChangeState();
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
                    SelectCard();
                    rewardButton.onClick.AddListener(() => selectCardPanel.transform.parent.gameObject.SetActive(true));
                
                    break;

                case 1:

                    var itemData = GameManager.Instance.dataManager.data.itemData;
                    var potionList = itemData.GetPotionData();
                    int randomPotionIndex = Random.Range(0, potionList.Count);
                    Instantiate( GameManager.Instance.ingameTopUI.listPotionPrefab[randomPotionIndex],rewardImage.transform.position, Quaternion.identity, rewardButton.transform);
                    rewardText.text = potionList[randomPotionIndex].name;
                    rewardButton.onClick.AddListener(() => AddPotion(randomPotionIndex));
                    rewardImage.gameObject.SetActive(false);
                    break;
            }
        }


        public void AddPotion(int radomPotionIndex)
        {
            var havePotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHavePotion;
            
            if(havePotion.Count >= 3)
            {
                Debug.Log("불가");
            }
            else
            {
                GameManager.Instance.ingameTopUI.AddPotion(radomPotionIndex);
                Destroy(rewardButton.gameObject);
            }
        }

        public void SelectCard()
        {
            int cardGoodsCount = 3;
            int allCardCount = GameManager.Instance.dataManager.data.cardData.cardCollect.listcardData.Count;
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            var itemData = GameManager.Instance.dataManager.data.itemData;

            for (int i = 0; i < cardGoodsCount; i++)
            {
                int randomCardIndex = Random.Range(0, allCardCount);
              
                CardBase cardBase = Instantiate(cardPrefab, selectCardPanel).GetComponent<CardBase>();
                cardBase.gameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
                cardBase.gameObject.AddComponent<Button>().onClick.AddListener(() => GameManager.Instance.ingameTopUI.AddCard(randomCardIndex));
                cardBase.gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(selectCardPanel.transform.parent.gameObject));
                cardBase.gameObject.GetComponent<Button>().onClick.AddListener(() => Destroy(rewardButton.gameObject.transform.gameObject));
                cardBase.Init(cardDatas[randomCardIndex]);
                ShopManager.ChangeSize(cardBase.gameObject, ItemType.Card, 0, itemData);
             
            }
        }

    }

    
}
