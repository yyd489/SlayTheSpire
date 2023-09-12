using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FrameWork
{
   
   
    public enum FieldEvenets
    {
        GoldEvent,
        FireEvenet,
        ClericEvent

    }

    public class StageManager : MonoBehaviour
    {
          public GameObject shopPop;
          public GameObject restPop;
          public GameObject[] arrEvents;

          public void ControlField(MapField fieldInfo)
          {

                switch(fieldInfo)
                {

                    case MapField.Event:
                    GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(false);
                    int randomEvent = Random.Range(0, arrEvents.Length);
                    Transform eventObj = Instantiate(arrEvents[randomEvent].transform);
                    eventObj.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(eventObj.gameObject));
                    eventObj.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ActiveEvent((FieldEvenets)randomEvent));
                    ControlEventText(eventObj, randomEvent);

                    break;

                    case MapField.Monster:

                    GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(true);
                    GameManager.Instance.spawnManager.Init(MapField.Monster);
                    GameManager.Instance.battleManager.Init();
                    GameManager.Instance.cardManager.StageStart();
                    break;

                    case MapField.EliteMonster:
                    GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(true);
                    GameManager.Instance.spawnManager.Init(MapField.EliteMonster);
                    GameManager.Instance.battleManager.Init();
                    GameManager.Instance.cardManager.StageStart();
                    break;

                    case MapField.Shop:

                    GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(false);
                    GameObject shopObj = Instantiate(shopPop);
                    //shopObj.transform.Find("ShopPanel").Find("BackButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(shopObj));
                    shopObj.transform.Find("Canvas").Find("NextButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(shopObj));
                   

                    
                    break;

                    case MapField.Sleep:
                    GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(false);
                    GameObject restObj = Instantiate(restPop);
                    restObj.transform.Find("RestButton").GetComponent<Button>().onClick.AddListener(() => FillHp());
                    restObj.transform.Find("RestButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(restObj));
                  

                    break;

                    case MapField.Boss:

                    GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(true);
                    GameManager.Instance.spawnManager.Init(MapField.Boss);
                    GameManager.Instance.soundManager.backgroundAudio.clip = GameManager.Instance.soundManager.backgroundSound[2];
                    GameManager.Instance.soundManager.backgroundAudio.Play();
                    //GameManager.Instance.cardManager.Init();
                    break;

                }
                

          }

        public void ClearStage(GameObject targetObj = null)
        {
            Destroy(targetObj);
            GameManager.Instance.ingameTopUI.mapPop.SetActive(true);

        }

        public void FillHp()
        {
            int fillHp = 30;
            int maxHp = GameManager.Instance.dataManager.data.characterData.GetCharacterStat().maxHp;
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillHp;

            if (GameManager.Instance.dataManager.data.characterData.GetCharacterStat().hp > maxHp)
            {
                GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp = maxHp;
            }

        }

        public void ControlEventText( Transform eventTransform,int eventType)
        {
            eventTransform.Find("EventTitleText").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.dataManager.data.eventData.GetEventData()[eventType].eventName;
            eventTransform.Find("TitleBackGround").Find("EventText").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.dataManager.data.eventData.GetEventData()[eventType].eventGuide;
            eventTransform.Find("EffectButton").Find("EffectText").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.dataManager.data.eventData.GetEventData()[eventType].effectText;
        }

        public void ActiveEvent(FieldEvenets eventType)
        {

            var characterData = GameManager.Instance.dataManager.data.characterData;
            switch (eventType)
            {
                case FieldEvenets.GoldEvent:

                    int rewardGold = 30;

                    characterData.characterInfoCollect.characterCollect.gold += rewardGold;
                    GameManager.Instance.ingameTopUI.goldText.text = characterData.characterInfoCollect.characterCollect.gold.ToString();
                    break;

                case FieldEvenets.FireEvenet:

                    int fillHp = 30;
                    characterData.characterInfoCollect.characterCollect.hp += fillHp;
                    
                    if (characterData.characterInfoCollect.characterCollect.hp > characterData.characterInfoCollect.characterCollect.maxHp)
                    {
                        characterData.characterInfoCollect.characterCollect.hp = characterData.characterInfoCollect.characterCollect.maxHp;

                    }
                    GameManager.Instance.ingameTopUI.nowHpText.text = characterData.characterInfoCollect.characterCollect.hp + "/" + characterData.characterInfoCollect.characterCollect.maxHp.ToString();
                    break;

                case FieldEvenets.ClericEvent:

                    int fillLittleHp = 10;

                    GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillLittleHp;
                    GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp += fillLittleHp;
                    GameManager.Instance.ingameTopUI.nowHpText.text = characterData.characterInfoCollect.characterCollect.hp + "/" + characterData.characterInfoCollect.characterCollect.maxHp.ToString();

                    break;
            }

        }
        // Start is called before the first frame update

    }
}
