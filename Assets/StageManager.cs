﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
                    GameObject eventObj = Instantiate(arrEvents[randomEvent]);
                    eventObj.transform.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(eventObj));
                    eventObj.transform.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ActiveEvent((FieldEvenets)randomEvent));

                    break;

                    case MapField.Monster:

                    break;

                    case MapField.EliteMonster:

                    break;

                    case MapField.Shop:

                    GameObject shopObj = Instantiate(shopPop);
                    shopObj.transform.Find("ShopPanel").Find("BackButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(shopObj));

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
            int maxHp = DataManager.data.characterData.AwaitGetCharacter().maxHp;
            DataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillHp;

            if (DataManager.data.characterData.AwaitGetCharacter().hp > maxHp)
            {
                DataManager.data.characterData.characterInfoCollect.characterCollect.hp = maxHp;
            }

        }

        public void ActiveEvent(FieldEvenets eventType)
        {
            switch(eventType)
            {
                case FieldEvenets.GoldEvent:

                    int rewardGold = 30;
                    DataManager.data.characterData.characterInfoCollect.characterCollect.gold += rewardGold;

                    break;

                case FieldEvenets.FireEvenet:
                    
                    int fillHp = 30;
                    DataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillHp;

                    break;

                case FieldEvenets.ClericEvent:

                    int fillLittleHp = 10;

                    DataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillLittleHp;
                    DataManager.data.characterData.characterInfoCollect.characterCollect.maxHp += fillLittleHp;

                    break;

            }


        }
        // Start is called before the first frame update

    }
}