using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
namespace FrameWork.Data
{
    public enum RelicType
    {
        HealFire,
        Twist,
        Accursed,
        Void
       
    }

    public enum PotionType
    {
       
        Fire,
        Heal,
        Card,
        None = 999
    }

    public class ItemDataCollect
    {
        public List<RelicJsonData> listRelicData;
        public List<PotionJsonData> listPotionData;
    }

    public class RelicJsonData
    {
        public string name;
        public int relicEffect;
        public RelicType relicType;
        public string effectGuide;
        public int cost;
    }

    public class PotionJsonData
    {
        public string name;
        public int potionEffect;
        public PotionType itemType;
        public string effectGuide;
        public int cost;
    }

    public class ItemData : DataInterface
    {
        public ItemDataCollect itemData;

        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            itemData = JsonConvert.DeserializeObject<ItemDataCollect>(fileTest);
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

        public List<RelicJsonData> GetRelicData()
        {
            // await UniTask.WaitUntil(() => characterInfoCollect != null);
            List<RelicJsonData> listRelicData = itemData.listRelicData;
            return listRelicData;
        }

        public List<PotionJsonData> GetPotionData()
        {
            // await UniTask.WaitUntil(() => characterInfoCollect != null);
            List<PotionJsonData> listPotionData = itemData.listPotionData;
            return listPotionData;
        }

        //public void WriteJson()
        //{

        //    relicData = new RelicDataCollect();
        //    relicData.listRelicData = new List<RelicJsonData>();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        relicData.listRelicData.Add(new RelicJsonData());
        //        relicData.listRelicData[i] = new RelicJsonData();
        //    }

        //    relicData.listRelicData[0].name = "꽈배기";
        //    relicData.listRelicData[0].relicType = relicType.Twist;
        //    relicData.listRelicData[0].relicEffect = 10;

        //    relicData.listRelicData[1].name = "꽈배기";
        //    relicData.listRelicData[1].relicType = relicType.Twist;
        //    relicData.listRelicData[1].relicEffect = 10;


        //    relicData.listRelicData[2].name = "저주받은 부적";
        //    relicData.listRelicData[2].relicType = relicType.Accursed;
        //    relicData.listRelicData[2].relicEffect = 5;


        //    relicData.listRelicData[3].name = "공허의 선물";
        //    relicData.listRelicData[3].relicType = relicType.Void;
        //    relicData.listRelicData[3].relicEffect = 10;

        //    string json = JsonConvert.SerializeObject(relicData);
        //    File.WriteAllText(Application.dataPath + "/DataJson/relicData.json", json);
        //}
    }


}
