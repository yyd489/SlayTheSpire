
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
            data.characterData.Init(UnityEngine.Application.streamingAssetsPath + "/DataJson/CharacterCollect.json");
            data.cardData = new CardData();
            data.cardData.Init(UnityEngine.Application.streamingAssetsPath + "/DataJson/cardData.json");
            data.eventData = new EventData();
            data.eventData.Init(UnityEngine.Application.streamingAssetsPath + "/DataJson/eventData.json");
            data.itemData = new ItemData();
            data.itemData.Init(UnityEngine.Application.streamingAssetsPath + "/DataJson/ItemData.json");
            data.monsterData = new MonsterData();
            data.monsterData.Init(UnityEngine.Application.streamingAssetsPath + "/DataJson/monsterData.json");


          
            // data.mapData.Init("Assets/DataJson/CharacterCollect.json");

        }

        public bool ReadyData()
        {
            if (data.characterData == null)
                return false;
            if (data.characterData == null)
                return false;
            if (data.cardData == null)
                return false;
            if (data.eventData == null)
                return false;
            if (data.itemData == null)
                return false;
            if (data.monsterData == null)
                return false;

            return true;
        }
        
    }

    [Serializable]
    public class DataList
    {
        public CharacterData characterData;
        public MapData mapData;
        public CardData cardData;
        public EventData eventData;
        public ItemData itemData;
        public MonsterData monsterData;
    }

    
  
}
