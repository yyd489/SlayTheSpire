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
          private System.Action[] arrStage = new System.Action[6];
          private System.Action<Data.CharacterData,FieldEvenets> eventStage;
       

        public void init()
        {
            arrStage[(int)MapField.Event] = OnEventStage;
            arrStage[(int)MapField.Monster] = OnMonsterStage;
            arrStage[(int)MapField.EliteMonster] = OnEliteMonsterStage;
            arrStage[(int)MapField.Shop] = OnShopStage;
            arrStage[(int)MapField.Sleep] = OnSleepStage;
            arrStage[(int)MapField.Boss] = OnBossStage;

            
            eventStage = OnEvent;
           
        }

        public void ControlField(MapField fieldInfo)
        {
            GameManager.Instance.battleManager.stage = fieldInfo;
            arrStage[(int)fieldInfo]();
         
        }

        public void OnEventStage()
        {
            GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(false);
            int randomEvent = Random.Range(0, arrEvents.Length);
            Transform eventObj = Instantiate(arrEvents[randomEvent].transform);
            eventObj.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(eventObj.gameObject));
            eventObj.Find("EffectButton").GetComponent<Button>().onClick.AddListener(() => ActiveEvent((FieldEvenets)randomEvent));
            ControlEventText(eventObj, randomEvent);
        }

        public void OnMonsterStage()
        {
            GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(true);
            GameManager.Instance.spawnManager.Init(MapField.Monster);
            GameManager.Instance.battleManager.Init();
            GameManager.Instance.cardManager.StageStart();
        }

        public void OnEliteMonsterStage()
        {
            GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(true);
            GameManager.Instance.spawnManager.Init(MapField.EliteMonster);
            GameManager.Instance.battleManager.Init();
            GameManager.Instance.cardManager.StageStart();
        }

        public void OnShopStage()
        {
            GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(false);
            GameObject shopObj = Instantiate(shopPop);
            shopObj.transform.Find("Canvas").Find("NextButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(shopObj));
        }

        public void OnSleepStage()
        {
            GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(false);
            GameObject restObj = Instantiate(restPop);
            restObj.transform.Find("RestButton").GetComponent<Button>().onClick.AddListener(() => FillHp());
            restObj.transform.Find("RestButton").GetComponent<Button>().onClick.AddListener(() => ClearStage(restObj));
        }

        public void OnBossStage()
        {
            GameManager.Instance.initilizer.inGameUiCanvas.gameObject.SetActive(true);
            GameManager.Instance.spawnManager.Init(MapField.Boss);
            GameManager.Instance.soundManager.backgroundAudio.clip = GameManager.Instance.soundManager.backgroundSound[2];
            GameManager.Instance.soundManager.backgroundAudio.Play();
            GameManager.Instance.battleManager.Init();
            GameManager.Instance.cardManager.StageStart();
        }

        public void ClearStage(GameObject targetObj = null)
        {
            Destroy(targetObj);
          //  GameManager.Instance.mapManager.ChangeLocalSize();
            GameManager.Instance.ingameTopUI.mapPop.SetActive(true);
            GameManager.Instance.mapManager.ChangeLocalSize();
        }

        public void FillHp()
        {
            int fillHp = 30;
            int maxHp = GameManager.Instance.dataManager.data.characterData.characterStat().maxHp;
            GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp += fillHp;

            if (GameManager.Instance.dataManager.data.characterData.characterStat().hp > maxHp)
            {
                GameManager.Instance.dataManager.data.characterData.characterInfoCollect.characterCollect.hp = maxHp;
            }

            GameManager.Instance.ingameTopUI.nowHpText.text = GameManager.Instance.dataManager.data.characterData.characterStat().hp + "/" + GameManager.Instance.dataManager.data.characterData.characterStat().maxHp.ToString();
            GameManager.Instance.playerControler.ironclad.Heal(0);
        }

        public void ControlEventText( Transform eventTransform,int eventType)
        {
            eventTransform.Find("EventTitleText").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.dataManager.data.eventData.eventData()[eventType].eventName;
            eventTransform.Find("TitleBackGround").Find("EventText").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.dataManager.data.eventData.eventData()[eventType].eventGuide;
            eventTransform.Find("EffectButton").Find("EffectText").GetComponent<TextMeshProUGUI>().text = GameManager.Instance.dataManager.data.eventData.eventData()[eventType].effectText;
        }

        public void ActiveEvent(FieldEvenets eventType)
        {
      
            var characterData = GameManager.Instance.dataManager.data.characterData;
            eventStage(characterData,eventType);
            GameManager.Instance.playerControler.ironclad.Heal(0);
           
        }


        private void OnEvent(Data.CharacterData characterData,FieldEvenets fieldEvnet)
        {
            switch (fieldEvnet)
            {
                case FieldEvenets.GoldEvent:
                    OnGoldEvent(characterData);
                    break;

                case FieldEvenets.FireEvenet:
                    OnFireEvent(characterData);
                    break;
                case FieldEvenets.ClericEvent:

                    OnClericEvent(characterData);
                    break;
            }
            
        }

        private void OnGoldEvent(Data.CharacterData characterData)
        {
            int rewardGold = 10;
            characterData.characterInfoCollect.characterCollect.gold += rewardGold;
            GameManager.Instance.ingameTopUI.goldText.text = characterData.characterInfoCollect.characterCollect.gold.ToString();
        }

        private void OnFireEvent(Data.CharacterData characterData)
        {
            int fillLittleHp = 10;

            characterData.characterInfoCollect.characterCollect.hp += fillLittleHp;
            characterData.characterInfoCollect.characterCollect.maxHp += fillLittleHp;
            GameManager.Instance.ingameTopUI.nowHpText.text = characterData.characterInfoCollect.characterCollect.hp + "/" + characterData.characterInfoCollect.characterCollect.maxHp.ToString();
            GameManager.Instance.playerControler.ironclad.Heal(0);
        }



        private void OnClericEvent(Data.CharacterData characterData)
        {
            int fillLittleHp = 20;

            characterData.characterInfoCollect.characterCollect.hp += fillLittleHp;
            if (characterData.characterInfoCollect.characterCollect.hp > characterData.characterInfoCollect.characterCollect.maxHp)
                characterData.characterInfoCollect.characterCollect.hp = characterData.characterInfoCollect.characterCollect.maxHp;
            GameManager.Instance.ingameTopUI.nowHpText.text = characterData.characterInfoCollect.characterCollect.hp + "/" + characterData.characterInfoCollect.characterCollect.maxHp.ToString();
            GameManager.Instance.playerControler.ironclad.Heal(0);
        }

        // Start is called before the first frame update

    }
}
