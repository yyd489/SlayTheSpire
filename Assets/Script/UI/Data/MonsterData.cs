using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
namespace FrameWork.Data
{
    public enum MonsterType
    {
        Normal,
        EliteMonster,
        BossMonster
    }

    public class MonsterDataCollect
    {
        public List<MonsterJsonData> listMonsterData;

    }
    [System.Serializable]
    public class MonsterJsonData
    {
       public string monsterName;
       public int monsterAttack;
       public int hp;
       public MonsterType monsterType;

    }

    public class MonsterData : DataInterface
    {
        public MonsterDataCollect monsterData;

        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            monsterData = JsonConvert.DeserializeObject<MonsterDataCollect>(fileTest);

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

        public List<MonsterJsonData> GetEventData()
        {
            // await UniTask.WaitUntil(() => characterInfoCollect != null);
            List<MonsterJsonData> listMonsterData = monsterData.listMonsterData;
            return listMonsterData;
        }
    }
}
