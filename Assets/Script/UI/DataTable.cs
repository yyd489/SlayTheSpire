
namespace FrameWork.Data
{
    using System;

    //using Newtonsoft.Json;
    public partial class DataManager
    {
        public static DataList data;

        public void Init()
        {
            LoadDataJson();
           
        }

        public void LoadDataJson()
        {
            data = new DataList();
            data.characterData = new CharacterData();
            data.characterData.Init("Assets/DataJson/CharacterCollect.json");
           // data.mapData.Init("Assets/DataJson/CharacterCollect.json");
           
        }
        
    }

    [Serializable]
    public class DataList
    {
        public CharacterData characterData;
        public MapData mapData;
     
    }
  
}
