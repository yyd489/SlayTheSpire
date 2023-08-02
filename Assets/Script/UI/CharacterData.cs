using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using UnityEngine;

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
        public int mp;
        public int attack;
        public int gold;
    }

    [Serializable]
    public class CharacterData : DataInterface
    {
        public CharacterInfoCollet characterInfoCollect;
       
        public async void Init(string path)
        {
            AwaitFileRead(path);

        }
        public async Task<CharacterCollect> AwaitGetCharacter()//이 함수의 실행함수가 async로 실행되야함.
        {
           await UniTask.WaitUntil(() => characterInfoCollect != null);
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

    }

}
