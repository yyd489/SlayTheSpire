﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace FrameWork
{
    using FrameWork.Data;

    public class IngameTopUI : MonoBehaviour
    {
        public Button optionButton;
        public Button deckButton;
        public Button mapButton;
        public GameObject mapPop;
        public GameObject optionPop;
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI nowHpText;
        public List<Sprite> listRelicSprites = new List<Sprite>();
        public List<GameObject> listPotionPrefab;
        public GameObject relicObject;
        public Transform potionsPanel;
        public GameObject itemTipPrefab;
    

        // Start is called before the first frame update

        public void Init()
        {
            ControlTopButton();
            ChangeState();
            if (mapPop == null)
            mapPop = GameManager.Instance.mapManager.gameObject;

            var listRelic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;
            int relicCount = listRelic.Count;
            var itemData = GameManager.Instance.dataManager.data.itemData;

            for (int i = 0; i < relicCount; i++)
            {
               Image relicImage = Instantiate(relicObject,GameObject.Find("RelicsPanel").transform).GetComponent<Image>();
                ShopManager.ChangeSize(relicImage.gameObject, ItemType.Relic, i, itemData);
                relicImage.sprite = listRelicSprites[(int)listRelic[i]]; 
            }

            optionButton.onClick.AddListener(() => GameManager.Instance.soundManager.effectPlaySound(2));
            deckButton.onClick.AddListener(() => GameManager.Instance.soundManager.effectPlaySound(2));
            mapButton.onClick.AddListener(() => GameManager.Instance.soundManager.effectPlaySound(0));

            var arrPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;


            //GameManager.Instance.ingameTopUI.AddPotion(0);
            //GameManager.Instance.ingameTopUI.AddPotion(1);
            //GameManager.Instance.ingameTopUI.AddPotion(2);
        }

        public void ControlTopButton()
        {
            optionButton = GameObject.Find("OptionButton").GetComponent<Button>();
            optionButton.onClick.AddListener(() => AsyncUIregister.InstansUI("Assets/Prefabs/UI/OptionCanvas.prefab"));

            deckButton = GameObject.Find("DeckButton").GetComponent<Button>();
            deckButton.onClick.AddListener(() => AsyncUIregister.InstansUI("Assets/Prefabs/UI/DeckPopup.prefab"));
            deckButton.onClick.AddListener(() => DeckPopUI.listDeckCards = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveCard);
            

            mapButton = GameObject.Find("MapButton").GetComponent<Button>();
            mapButton.onClick.AddListener(() => mapPop.gameObject.SetActive(true));
            mapButton.onClick.AddListener(() => GameManager.Instance.mapManager.ChangeLocalSize());
          
        }

        public void ChangeState()
        {
            goldText.text = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.gold.ToString();
            nowHpText.text = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp.ToString() +"/" +
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp.ToString();
        }

        public void ChangePotion()
        {
           
            var arrPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;
            var itemData = GameManager.Instance.dataManager.data.itemData;

            for (int i = potionsPanel.childCount; i <arrPotion.Length; i++)
            {
                if (arrPotion[i] != PotionType.None)
                {
                    GameObject potionObject = Instantiate(listPotionPrefab[(int)arrPotion[i]], potionsPanel);
                    potionObject.AddComponent<Button>().onClick.AddListener(() => GameManager.Instance.potionManager.PopUpPotionUi(potionObject.transform.GetSiblingIndex()));
                    ShopManager.ChangeSize(potionObject, ItemType.Potion, i, itemData);
                }
           }
        }

        public void ChangeRelic()
        {
            var listRelic = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.listHaveRelic;
            var relicsPanel = GameObject.Find("RelicsPanel").transform;
            var itemData = GameManager.Instance.dataManager.data.itemData;

            for (int i = relicsPanel.childCount; i < listRelic.Count; i++)
            {
                Image relicImage = Instantiate(relicObject, GameObject.Find("RelicsPanel").transform).GetComponent<Image>();
                relicImage.sprite = listRelicSprites[(int)listRelic[i]];
                ShopManager.ChangeSize(relicImage.gameObject, ItemType.Relic, i, itemData);
            }
        }

        public void AddCard(int itemDataIndex)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            characterData.listHaveCard.Add(itemDataIndex);
        }

        public void AddPotion(int itemDataIndex)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();

            for (int i = 0; i < characterData.arrHavePotion.Length; i++)
            {
                if (characterData.arrHavePotion[i] == PotionType.None)
                {
                    characterData.arrHavePotion[i] = (PotionType)itemDataIndex;
                    break;
                }

                else if (i == characterData.arrHavePotion.Length - 1 && characterData.arrHavePotion[i] != PotionType.None)
                {
                    StartCoroutine(OnPotionBlock());
                }

            }
                ChangePotion();
        }

        public void AddRelic(int itemDataIndex)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            characterData.listHaveRelic.Add((RelicType)itemDataIndex);
            ChangeRelic();
        }

       public IEnumerator OnPotionBlock()
        {
           AsyncUIregister.InstansUI("Assets/Prefabs/UI/PotionBlock.prefab",this.gameObject.transform.parent);

            yield return new WaitUntil(() => this.transform.parent.Find("PotionBlock(Clone)") != null);

           var potionOject = this.transform.parent.Find("PotionBlock(Clone)").transform.GetComponent<CanvasGroup>();

        

           potionOject.DOFade(1, 0.2f);

           yield return new WaitUntil(() => potionOject.alpha == 1);

           potionOject.DOFade(0, 0.2f);
        }

       
    }
}

