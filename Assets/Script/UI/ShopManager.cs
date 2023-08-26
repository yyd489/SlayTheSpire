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
    public class ShopManager : MonoBehaviour
    {
        public Transform cardPanel;
        public Transform cardCostParent;
        public GameObject cardPrefab;
        public static bool activeShop;
        public Sprite soldOut;
        public Image deleteCardButton;
        public static GameObject saveBeforeCard;
        public Transform relicPanel;
        public Transform potionPanel;
        public Transform itemCostBox;


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
            
            

            for (int i = 0; i < cardGoodsCount; i++)
            {
                int randomCardIndex = Random.Range(0, allCardCount);
                int cost = cardDatas[randomCardIndex].cost;
                CardBase cardBase = Instantiate(cardPrefab, cardPanel).GetComponent<CardBase>();
                cardBase.gameObject.GetComponent<UnityEngine.EventSystems.EventTrigger>().enabled = false;
              
                cardBase.gameObject.AddComponent<Button>().onClick.AddListener(() => AddItem(cost,cardBase.gameObject,randomCardIndex,cardPanel,0));
                cardBase.cardManager = cardManager;
                cardBase.Init(cardDatas[randomCardIndex]);
                cardCostParent.GetChild(i).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;
                ChangeSize(cardBase.gameObject);
                
            }

            var relicList = GameManager.Instance.dataManager.data.itemData.GetRelicData();
            var potionList = GameManager.Instance.dataManager.data.itemData.GetPotionData();
            var relicPrefab = GameManager.Instance.ingameUI.relicObject;
            for(int i = 1; i < relicList.Count; i++)
            {
                int randomRelicIndex = Random.Range(1,relicList.Count);
                int cost = relicList[randomRelicIndex].cost;
                GameObject relicObj = Instantiate(relicPrefab, relicPanel);
                relicObj.GetComponent<Image>().sprite = GameManager.Instance.ingameUI.listRelicSprites[randomRelicIndex];
                relicObj.AddComponent<Button>().onClick.AddListener(() => AddItem(cost, relicObj, randomRelicIndex, cardPanel,1));
                ChangeSize(relicObj);

                itemCostBox.GetChild(i-1).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;
                //ChangeSize(cardBase.gameObject);
                // Instantiate(relicPrefab, ).GetComponent<CardBase>();
                //GameObject potionObj = Instantiate(cardPrefab, cardPanel).gameObject
            }

            Debug.Log(potionList.Count);

            for (int i = 0; i < potionList.Count; i++)
            {
                int randomPotionIndex = Random.Range(0, potionList.Count);
                int cost = potionList[randomPotionIndex].cost;
                var potionObject = GameManager.Instance.ingameUI.listPotionPrefab[randomPotionIndex];
                GameObject potionObj = Instantiate(potionObject, potionPanel);
                potionObj.AddComponent<Button>().onClick.AddListener(() => AddItem(cost, potionObj, randomPotionIndex, cardPanel,2));
                itemCostBox.GetChild(i + 3).Find("CostText").GetComponent<TextMeshProUGUI>().text = "" + cost;

                ChangeSize(potionObj);
            }

         
        }

        public void AddItem(int cost, GameObject item,int itemDataIndex,Transform itemPanel,int itemType)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            int gold = characterData.gold;
           
            int itemIndex = item.transform.GetSiblingIndex();

            if(gold >= cost)
            {
                Destroy(item);
                Destroy(itemPanel.GetChild(itemIndex).gameObject);
                GameManager.Instance.ingameUI.AddCard(itemDataIndex);
                characterData.gold -= cost;
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
            }
        }

        public static void ChangeSize(GameObject Obj)
        {
            EventTrigger eventTrigger = Obj.AddComponent<EventTrigger>();
            EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
            entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
            entry_PointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
            eventTrigger.triggers.Add(entry_PointerEnter);

            EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();
            entry_PointerExit.eventID = EventTriggerType.PointerExit;
            entry_PointerExit.callback.AddListener((data) => { OnPointerExit(); });
            eventTrigger.triggers.Add(entry_PointerExit);

        }
            

        public static void OnPointerEnter(PointerEventData data)
        {
           data.pointerEnter.gameObject.transform.DOScale(0.75f, 0.25f);// = new Vector3(0.7f, 0.7f, 0.7f);
           saveBeforeCard = data.pointerEnter.gameObject;    
        }

        public static void OnPointerExit()
        {
          
            saveBeforeCard.transform.DOScale(0.5f, 0.5f);
        }

        
    }
}
