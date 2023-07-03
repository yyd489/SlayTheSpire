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

            //var aas = GetData<CharacterCollect>(dataManager.data.characterData.characterInfoCollect.characterCollect);


            //StartCoroutine(Example(aas));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.T))
            {
               
            }
        }


    }
}
