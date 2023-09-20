using System.Collections;
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
            var gameManagerInstance = GameManager.Instance;
            if (mapPop == null)
                mapPop = gameManagerInstance.mapManager.gameObject;

            var listRelic = gameManagerInstance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;
            int relicCount = listRelic.Count;
            var itemData = gameManagerInstance.dataManager.data.itemData;
            

            for (int i = 0; i < relicCount; i++)
            {
                Image relicImage = Instantiate(relicObject, GameObject.Find("RelicsPanel").transform).GetComponent<Image>();
                ShopManager.ChangeSize(relicImage.gameObject, ItemType.Relic, i, itemData);
                relicImage.sprite = listRelicSprites[(int)listRelic[i]];
            }

            var soundManager = gameManagerInstance.soundManager;

            optionButton.onClick.AddListener(() => soundManager.effectPlaySound(2));
            deckButton.onClick.AddListener(() => soundManager.effectPlaySound(2));
            mapButton.onClick.AddListener(() => soundManager.effectPlaySound(0));

            var arrPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;

            var ingameTopUI = gameManagerInstance.ingameTopUI;

            ingameTopUI.AddPotion(0);
            ingameTopUI.AddPotion(1);
            ingameTopUI.AddPotion(2);


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
            var character = GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect;

            goldText.text = character.gold.ToString();
            nowHpText.text = character.hp.ToString() + "/" + character.maxHp.ToString();
        }



        public void ChangeRelic()
        {
            var listRelic = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().listHaveRelic;
            var relicsPanel = GameObject.Find("RelicsPanel").transform;
            var itemData = GameManager.Instance.dataManager.data.itemData;


            for (int i = relicsPanel.childCount; i < listRelic.Count; i++)
            {
                Image relicImage = Instantiate(relicObject, GameObject.Find("RelicsPanel").transform).GetComponent<Image>();
                relicImage.sprite = listRelicSprites[(int)listRelic[i]];
                ShopManager.ChangeSize(relicImage.gameObject, ItemType.Relic, (int)listRelic[i], itemData);
            }
        }

        public void AddCard(int itemDataIndex)
        {
 
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();

            characterData.listHaveCard.Add(itemDataIndex);

        }

        public void AddPotion(int itemDataIndex)
        {
            var havePotions = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;


            for (int i = 0; i < havePotions.Length; i++)
            {
                if (havePotions[i] == PotionType.None)
                {

                    havePotions[i] = (PotionType)itemDataIndex;
                    ChangePotion(itemDataIndex, i);
                    break;
                }

                else if (i == havePotions.Length - 1 && havePotions[i] != PotionType.None)
                {

                    StartCoroutine(OnPotionBlock());
                }

            }
         
        }

        public void ChangePotion(int itemDataIndex,int index)
        {
            var arrPotion = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().arrHavePotion;
            var itemData = GameManager.Instance.dataManager.data.itemData;

            GameObject potionObject = Instantiate(listPotionPrefab[itemDataIndex], potionsPanel);
            Transform potionTr = potionObject.transform;
            potionTr.localScale = new Vector2(0.3f, 0.3f);
            potionTr.SetSiblingIndex(index);
            potionObject.name = index.ToString();

            potionObject.AddComponent<Button>().onClick.AddListener(() => GameManager.Instance.potionManager.PopUpPotionUi(potionObject, int.Parse(potionObject.name)));
            ShopManager.ChangeSize(potionObject, ItemType.Potion, itemDataIndex, itemData);


            for (int i = 0; i < potionsPanel.transform.childCount; i++)
            {
                potionsPanel.transform.GetChild(i).transform.position = new Vector2(potionsPanel.transform.position.x + (i * 70) - 70, potionsPanel.transform.position.y);
            }

            //for (int i = 0; i < arrPotion.Length; i++)
            //    Debug.Log(arrPotion[i]);
        }

        public void AddRelic(int itemDataIndex)
        {
            var characterData = GameManager.Instance.dataManager.data.characterData.GetCharacterStat();
            characterData.listHaveRelic.Add((RelicType)itemDataIndex);
            if((RelicType)itemDataIndex == RelicType.Twist)
            {
                GameManager.Instance.playerControler.ironclad.RelicHealthUp();
             
            }

            ChangeRelic();
        }

        public IEnumerator OnPotionBlock()
        {
            if (this.transform.parent.Find("PotionBlock(Clone)") != null)
            {
                Destroy(this.transform.parent.Find("PotionBlock(Clone)").gameObject);
            }

            AsyncUIregister.InstansUI("Assets/Prefabs/UI/PotionBlock.prefab", this.gameObject.transform.parent);

            yield return new WaitUntil(() => this.transform.parent.Find("PotionBlock(Clone)") != null);

            var potionOject = this.transform.parent.Find("PotionBlock(Clone)").transform.GetComponent<CanvasGroup>();

            potionOject.DOFade(1, 0.2f);

            yield return new WaitUntil(() => potionOject.alpha == 1);

            potionOject.DOFade(0, 0.2f);

            yield return new WaitUntil(() => potionOject.alpha == 0);

            Destroy(potionOject.gameObject);
        }
    }
}

