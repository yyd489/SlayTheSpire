﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FrameWork
{
    using FrameWork.Data;
   
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

                    int randomEvent = Random.Range(0, arrEvents.Length);
                    Transform eventObj = Instantiate(arrEvents[randomEvent].transform);
                    eventObj.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(eventObj.gameObject));
                    eventObj.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ActiveEvent((FieldEvenets)randomEvent));
                    ControlEventText(eventObj, randomEvent);

                    break;

                    case MapField.Monster:

                    break;

                    case MapField.EliteMonster:

                    break;

                    case MapField.Shop:

                    GameObject shopObj = Instantiate(shopPop);
                    //shopObj.transform.Find("ShopPanel").Find("BackButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(shopObj));
                    shopObj.transform.Find("Canvas").Find("NextButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(shopObj));
                    
                    break;

                    case MapField.Sleep:

                    GameObject restObj = Instantiate(restPop);
                    restObj.transform.Find("RestButton").GetComponent<Button>().onClick.AddListener(() => FillHp());
                    restObj.transform.Find("RestButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(restObj));
                   
                    break;

                    case MapField.Boss:

                    break;

                }
                

          }

        public void ClearStage(GameObject targetObj = null)
        {
            Destroy(targetObj);
            GameManager.Instance.ingameUI.mapPop.SetActive(true);

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
            switch (eventType)
            {
                case FieldEvenets.GoldEvent:

                    int rewardGold = 30;
                    GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.gold += rewardGold;

                    break;

                case FieldEvenets.FireEvenet:

                    int fillHp = 30;
                    GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillHp;

                    break;

                case FieldEvenets.ClericEvent:

                    int fillLittleHp = 10;

                    GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillLittleHp;
                    GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.maxHp += fillLittleHp;
                    
                    break;
            }

        }
        // Start is called before the first frame update

    }
}
