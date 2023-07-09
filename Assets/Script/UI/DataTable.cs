using Newtonsoft.Json;
using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
namespace FrameWork.Data
{
    using System;

    //using Newtonsoft.Json;
    public partial class DataManager
    {
        public DataList data;

        public void Init()
        {
            LoadDataJson();
           
        }

        public void LoadDataJson()
        {
            data = new DataList();
            data.characterData = new CharacterData();
            data.characterData.Init("Assets/DataJson/CharacterCollect.json"); 
        }
        //public static async  aas <T>(T value =null) where T : class
        //{
        //    await UniTask.WaitUntil(() => value != null);
        //    Debug.Log(value);
        //}

       

    }

    [Serializable]
    public class DataList
    {
        public CharacterData characterData;
    }
  
}
