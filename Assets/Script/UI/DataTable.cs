using Newtonsoft.Json;
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

        public static T GetData<T>(T value) where T : class
        {       
            //사용법
            //var Temp = GetData<CharacterCollect>(GameManager.dataManager.data.characterData.characterInfoCollect.characterCollect);
            //var attack = Temp.attack;
            //var hp = Temp.hp...;
            return value;
        }

    }



    [Serializable]
    public class DataList
    {
        public CharacterData characterData;
    }
  

   

}
