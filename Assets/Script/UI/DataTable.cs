using Newtonsoft.Json;
namespace FrameWork.Data
{
    using System;

    //using Newtonsoft.Json;
    public partial class DataManager
    {
        public void LoadDataJson()
        {
            DataList data = new DataList();
            data.characterData = new CharacterData();
            data.characterData.Init("Assets/DataJson/CharacterCollect.json");

            
        }

    }

    [Serializable]
    public class DataList
    {
        public CharacterData characterData;
    }
  



}
