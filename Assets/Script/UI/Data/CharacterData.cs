using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace FrameWork.Data
{
    interface DataInterface
    {   
        void AwaitFileRead(string filePath);
        void Init(string path);
        Task<string>ReadAllTextAsync(string filepath);
    }

    [Serializable]
    public class CharacterInfoCollet
    {
        public CharacterCollect characterCollect;
    }

    [Serializable]
    public class CharacterCollect
    {
        public string name;
        public int hp;
        public int maxHp = 88;
        public int mp;
        public int gold;
        public Dictionary<RelicType, int> dicHaveRelic = new Dictionary<RelicType, int>();
        public Dictionary<PotionType, int> dicHavePotion = new Dictionary<PotionType, int>();
        public List<int> listHaveCard = new List<int>();
    }

    [Serializable]
    public class CharacterData : DataInterface
    {
        public CharacterInfoCollet characterInfoCollect;
       
        public void Init(string path)
        {
            AwaitFileRead(path);
           
        }
        public CharacterCollect GetCharacterStat()//이 함수의 실행함수가 async로 실행되야함.
        {
          // await UniTask.WaitUntil(() => characterInfoCollect != null);
           CharacterCollect character = characterInfoCollect.characterCollect;
           return character;
        }

        public async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            characterInfoCollect = JsonConvert.DeserializeObject<CharacterInfoCollet>(fileTest);
            
        }

        public Task<string> ReadAllTextAsync(string filepath)
        {
            return Task.Factory.StartNew(() =>
            {
                return File.ReadAllText(filepath);
            });
        }
        //public void WriteJson()
        //{

        //    characterInfoCollect = new CharacterInfoCollet();
        //    characterInfoCollect.characterCollect = new CharacterCollect();

        //    characterInfoCollect.characterCollect.hp = 88;
        //    characterInfoCollect.characterCollect.maxHp = 88;
        //    characterInfoCollect.characterCollect.gold = 0;
        //    characterInfoCollect.characterCollect.name = "아이언클래드";
        //    characterInfoCollect.characterCollect.dicHaveRelic.Add(relicType.HealFire, 10);
        //    characterInfoCollect.characterCollect.dicHaveRelic.Add(relicType.Twist, 10);
        //    characterInfoCollect.characterCollect.mp = 3;

        //    for (int i = 0; i < 5; i++)
        //    {
        //        characterInfoCollect.characterCollect.listHaveCard.Add(2);
        //    }

        //    for (int i = 0; i < 4; i++)
        //    {
        //        characterInfoCollect.characterCollect.listHaveCard.Add(1);
        //    }


        //    characterInfoCollect.characterCollect.listHaveCard.Add(0);



        //    string json = JsonConvert.SerializeObject(characterInfoCollect);
        //    File.WriteAllText(UnityEngine.Application.dataPath + "/DataJson/CharacterCollect.json", json);
        //}
    }

}
