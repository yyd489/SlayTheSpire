using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
namespace FrameWork.Data
{
    public enum relicType
    {
        HealFire,
        Twist,
        Accursed,
        Void
       
    }

    public class RelicDataCollect
    {
        public List<RelicJsonData> listRelicData;

    }

    public class RelicJsonData
    {
        public string name;
        public int relicEffect;
        public relicType relicType;

    }

    public class RelicsData : DataInterface
    {
        public RelicDataCollect relicData;

        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            relicData = JsonConvert.DeserializeObject<RelicDataCollect>(fileTest);
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

        public List<RelicJsonData> GetEventData()
        {
            // await UniTask.WaitUntil(() => characterInfoCollect != null);
            List<RelicJsonData> listRelicData = relicData.listRelicData;
            return listRelicData;
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
