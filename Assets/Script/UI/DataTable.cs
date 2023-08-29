﻿
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
            data.cardData = new CardData();
            data.cardData.Init("Assets/DataJson/cardData.json");
            data.eventData = new EventData();
            data.eventData.Init("Assets/DataJson/eventData.json");
            data.itemData = new ItemData();
            data.itemData.Init("Assets/DataJson/ItemData.json");
            data.monsterData = new MonsterData();
            data.monsterData.Init("Assets/DataJson/monsterData.json");

            // data.mapData.Init("Assets/DataJson/CharacterCollect.json");

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
