using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    using FrameWork.Data;
    public class GameManager : MonoSingleton<GameManager>
    {
        public static DataManager dataManager { get; private set; }

        private void Start()
        {
            Init();
        
        }

        public void Init()
        {
            dataManager = new DataManager();
            dataManager.Init();

           // var ss = dataManager.data.characterData;
           //var ss = await dataManager.data.characterData.AwaitGetCharacter();
           //Debug.Log(ss.attack)
           // Debug.Log(ss.Result.attack);
           // var Temp = DataManager.GetData<CharacterInfoCollet>(GameManager.dataManager.data.characterData.characterInfoCollect);
          

        }



    }
}
