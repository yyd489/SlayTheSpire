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
        public CharacterInfoCollet characterCollect;

        public void Init(string path)
        {
            //AwaitFileRead(path);   

            var stts = AwaitFileRead("Assets/DataJson/CharacterCollect.json").Result;
            Debug.Log(stts);
        }

        private async Task<string> AwaitFileRead(string filePath)
        {
            var fileTest = await ReadAllTextAsync(filePath);
            characterCollect = JsonConvert.DeserializeObject<CharacterInfoCollet>(fileTest);

            return fileTest;
         
        }
        static Task<string> ReadAllTextAsync(string filepath)
        {
            return Task.Factory.StartNew(() =>
            {
                // 텍스트 파일 불러올 시 한글 깨질 때, Encoind.Default 추가
                return File.ReadAllText(filepath);
            });
        }

    }

}
