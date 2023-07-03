using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace FrameWork.Data
{
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
        public int mp;
        public int attack;
    }

    [Serializable]
    public class CharacterData
    {
        public CharacterInfoCollet characterInfoCollect;

        public void Init(string path)
        {
            AwaitFileRead(path);

            //GameManager.dataManager
        }

        private async void AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            characterInfoCollect = JsonConvert.DeserializeObject<CharacterInfoCollet>(fileTest);
            DataManager.GetData<CharacterCollect>(characterInfoCollect.characterCollect);
        }

        static Task<string> ReadAllTextAsync(string filepath)
        {
            return Task.Factory.StartNew(() =>
            {
                return File.ReadAllText(filepath);
            });
        }

       

    }

}
