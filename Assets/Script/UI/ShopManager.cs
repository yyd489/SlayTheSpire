using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using DG.Tweening;
namespace FrameWork
{
   
    using FrameWork.Data;

    public enum ItemType
    {
        Card,
        Relic,
        Potion

    }


    public class ShopManager : MonoBehaviour
    {
        public Transform cardPanel;
        public Transform cardCostBox;
        public GameObject cardPrefab;
        public static bool activeShop;
        public Sprite soldOut;
        public Image deleteCardButton;
        public static GameObject saveBeforeCard;
        public Transform relicPanel;
        public Transform potionPanel;
        public Transform shopPanel;
        public Transform relicCostBox;
        public Transform PotionCostBox;
       

        private void Start()
        {
            Init();
        }
        public void Init()

        {
            List<CardJsonData> cardDatas = GameManager.Instance.dataManager.data.cardData.GetCardStat();
            int allCardCount = GameManager.Instance.dataManager.data.cardData.cardCollect.listcardData.Count;
            CardManager cardManager = GameManager.Instance.cardManager;
            int cardGoodsCount = 5;

            var itemData = GameManager.Instance.dataManager.data.itemData;
            var relicList = itemData.GetRelicData();
            var potionList = itemData.GetPotionData();
            var relicPrefab = GameManager.Instance.ingameUI.relicObject;

            for (int i = 0; i < cardGoodsCount; i++)
            {
                int randomCardIndex = Random.Range(0, allCardCount);
                int cost = cardDatas[randomCardIndex].cost;
                CardBase cardBase = Instantiate(cardPrefab, cardPanel).GetComponent<CardBase>();
                cardBase.gameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
              
                cardBase.gameObject.AddComponent<Button>().onClick.AddListener(() => AddItem(cost,cardBase.gameObject,randomCardIndex,cardPanel, ItemType.Card, cardCostBox));
                cardBase.cardManager = cardManager;
                cardBase.Init(cardDatas[randomCardIndex]);
                cardCostBox.GetChild(i).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;
                ChangeSize(cardBase.gameObject, ItemType.Card, 0,itemData);
                
            }

            for(int i = 1; i < relicList.Count; i++)
            {
                int randomRelicIndex = Random.Range(1,relicList.Count);
                int cost = relicList[randomRelicIndex].cost;
                GameObject relicObj = Instantiate(relicPrefab, relicPanel);
                relicObj.GetComponent<Image>().sprite = GameManager.Instance.ingameUI.listRelicSprites[randomRelicIndex];
                relicObj.AddComponent<Button>().onClick.AddListener(() => AddItem(cost, relicObj, randomRelicIndex, relicPanel, ItemType.Relic, relicCostBox));
                ChangeSize(relicObj, ItemType.Relic, randomRelicIndex, itemData);
                relicCostBox.GetChild(i-1).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;
               
            }

            

            for (int i = 0; i < potionList.Count; i++)
            {
                int randomPotionIndex = Random.Range(0, potionList.Count);
                int cost = potionList[randomPotionIndex].cost;
                var potionObject = GameManager.Instance.ingameUI.listPotionPrefab[randomPotionIndex];
                GameObject potionObj = Instantiate(potionObject, potionPanel);
                potionObj.AddComponent<Button>().onClick.AddListener(() => AddItem(cost, potionObj, randomPotionIndex, potionPanel,ItemType.Potion ,PotionCostBox));
                PotionCostBox.GetChild(i).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;
                ChangeSize(potionObj, ItemType.Potion, randomPotionIndex, itemData);
            }

            StartCoroutine(UnActiveGrid());

        }

        IEnumerator UnActiveGrid()
        {
            yield return new WaitUntil(()=> shopPanel.gameObject.activeSelf == true);
            yield return new WaitForSeconds(0.1f);

            cardPanel.GetComponent<GridLayoutGroup>().enabled = false;
            relicPanel.GetComponent<GridLayoutGroup>().enabled = false;
            potionPanel.GetComponent<GridLayoutGroup>().enabled = false;
            cardCostBox.GetComponent<GridLayoutGroup>().enabled = false;
            relicCostBox.GetComponent<GridLayoutGroup>().enabled = false;
            PotionCostBox.GetComponent<GridLayoutGroup>().enabled = false;
        }

        public void AddItem(int cost, GameObject item, int itemDataIndex, Transform itemPanel, ItemType itemType, Transform textPanel)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            
            int gold = characterData.gold;
            int itemIndex = item.transform.GetSiblingIndex();
            
            if(gold >= cost)
            {
                Destroy(item);
                Destroy(itemPanel.GetChild(itemIndex).gameObject);
                
                characterData.gold -= cost;
                GameManager.Instance.ingameUI.ChangeState();
                if ( itemType == ItemType.Card)//카드
                {
                    GameManager.Instance.ingameUI.AddCard(itemDataIndex);
                    Destroy(textPanel.GetChild(itemIndex).gameObject);
                }

                else if (itemType == ItemType.Relic)//유물
                { 
                    GameManager.Instance.ingameUI.AddRelic(itemDataIndex);
                    Destroy(textPanel.GetChild(itemIndex).gameObject);
                }

                else if (itemType == ItemType.Potion)//포션
                {  
                    GameManager.Instance.ingameUI.AddPotion(itemDataIndex);
                    Destroy(textPanel.GetChild(itemIndex).gameObject);  
                }
            }
           
        }

        public void DeleteCardButton()
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            int gold = characterData.gold;
            int cost = 75;

            if (gold >= cost)
            {
                activeShop = true;
                GameManager.Instance.ingameUI.deckButton.onClick.Invoke();
                characterData.gold -= cost;
                deleteCardButton.sprite = soldOut;
                GameManager.Instance.ingameUI.ChangeState();
            }
        }

        public static void ChangeSize(GameObject Obj,ItemType itemType, int itemDataIndex =0, ItemData itemdata = default)
        {
            EventTrigger eventTrigger = Obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
            entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
            entry_PointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data, itemType, itemdata, itemDataIndex); });
            eventTrigger.triggers.Add(entry_PointerEnter);

            EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();
            entry_PointerExit.eventID = EventTriggerType.PointerExit;
            entry_PointerExit.callback.AddListener((data) => { OnPointerExit(); });
            eventTrigger.triggers.Add(entry_PointerExit);

        }
            

        public static void OnPointerEnter(PointerEventData data ,ItemType itemType , ItemData itemData ,int itemDataIndex)
        {
            GameObject dataObject = data.pointerEnter.gameObject;

           dataObject.transform.DOScale(0.75f, 0.25f);// = new Vector3(0.7f, 0.7f, 0.7f);
           saveBeforeCard = data.pointerEnter.gameObject;
          // var Index = data.pointerEnter.transform.GetSiblingIndex();

            switch(itemType)
            {
                case ItemType.Potion:

               
                    var itemGuide = GameManager.Instance.ingameUI.itemTipPrefab;
                    itemGuide.transform.position = new Vector2(dataObject.transform.position.x + 250, dataObject.transform.position.y);
                    var titleText = itemData.GetPotionData()[itemDataIndex].name;
                    var guideText = itemData.GetPotionData()[itemDataIndex].effectGuide;
                    itemGuide.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = titleText;
                    itemGuide.transform.Find("ItemGuide").GetComponent<TextMeshProUGUI>().text = guideText;
                    itemGuide.SetActive(true);

                    break;

                case ItemType.Relic:

                    itemGuide = GameManager.Instance.ingameUI.itemTipPrefab;
                    itemGuide.transform.position = new Vector2(dataObject.transform.position.x + 250, dataObject.transform.position.y);
                    titleText = itemData.GetRelicData()[itemDataIndex].name;
                    guideText = itemData.GetRelicData()[itemDataIndex].effectGuide;
                    itemGuide.transform.Find("TitleText").GetComponent<TextMeshProUGUI>().text = titleText;
                    itemGuide.transform.Find("ItemGuide").GetComponent<TextMeshProUGUI>().text = guideText;
                    itemGuide.SetActive(true);
                    break;
            }
           
        }

        public static void OnPointerExit()
        {

            GameManager.Instance.ingameUI.itemTipPrefab.SetActive(false);
            saveBeforeCard.transform.DOScale(0.5f, 0.5f);
        }

        
    }
}
