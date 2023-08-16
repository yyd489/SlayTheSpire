using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
namespace FrameWork.Data
{
    public enum CardType
    {
        Attack,
        Skill,
        Defence
    }

    [System.Serializable]
    public class CardDataCollect
    {
        public List<CardJsonData> listcardData;

    }
    [System.Serializable]
    public class CardJsonData//선수들의 스텟 및 정보
    {
        public string cardName;
        public CardType cardType;
        public string cardGuide;
        public int cardCost;
        public int cardEffect;
        public int cardSubEffect;
        public bool canDelete;
       

    }


    public class CardData : DataInterface
    {
        public CardDataCollect cardCollect;

        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            cardCollect = JsonConvert.DeserializeObject<CardDataCollect>(fileTest);
        
        }

        public void Init(string path)
        {
            AwaitFileRead(path);

        }

        public Task<string> ReadAllTextAsync(string filepath)
        {
            return Task.Factory.StartNew(() =>
            {
                return File.ReadAllText(filepath);
            });
        }

        public List<CardJsonData> GetCardStat()
        {
            // await UniTask.WaitUntil(() => characterInfoCollect != null);
            List<CardJsonData> listCardData = cardCollect.listcardData;
            return listCardData;
        }

        //public void WriteJson()
        //{

        //    cardCollect = new CardDataCollect();
        //    cardCollect.listcardData = new List<CardJsonData>();
        //    for (int i = 0; i < 7; i++)
        //    {
        //        cardCollect.listcardData.Add(new CardJsonData());
        //        cardCollect.listcardData[i] = new CardJsonData();
        //    }

        //    cardCollect.listcardData[0].cardCost = 2;
        //    cardCollect.listcardData[0].cardEffect = 8;
        //    cardCollect.listcardData[0].cardSubEffect = 2;
        //    cardCollect.listcardData[0].cardCost = 2;
        //    cardCollect.listcardData[0].canDelete = false;
        //    cardCollect.listcardData[0].cardName = "강타";
        //    cardCollect.listcardData[0].cardGuide = "피해를 8 부여합니다." + "<color=yellow> 취약을 2 부여합니다. : </color>";
        //    cardCollect.listcardData[0].cardType = CardType.Attack;

        //    cardCollect.listcardData[1].cardCost = 1;
        //    cardCollect.listcardData[1].cardEffect = 5;
        //    cardCollect.listcardData[1].cardSubEffect = 0;
        //    cardCollect.listcardData[1].cardCost = 1;
        //    cardCollect.listcardData[1].canDelete = false;
        //    cardCollect.listcardData[1].cardName = "수비";
        //    cardCollect.listcardData[1].cardGuide = "방어도를 5 얻습니다.";
        //    cardCollect.listcardData[1].cardType = CardType.Skill;

        //    string json = JsonConvert.SerializeObject(cardCollect);
        //    File.WriteAllText(Application.dataPath+"/DataJson/cardData.json", json);
        //}

    }
}
