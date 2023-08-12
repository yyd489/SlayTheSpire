using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrameWork
{
    using FrameWork.Data;
    public class GameManager : MonoSingleton<GameManager>
    {

        public DataManager dataManager { get; private set; }
        public Soundmanager soundManager { get; private set; }
        public IngameTopUI ingameUI { get; private set; }
        

        public StageManager stageManager { get; private set; }
        public PlayerControler playerControler { get; private set; }
        public CardSorting cardSorting { get; private set; }

        [SerializeField] Initializer initilizer;

       

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }
            dataManager.Init();

            //if (soundManager == null)
            //{
            //    soundManager = new Soundmanager();
            //}
            //
            //soundManager.Init(initilizer);
            //
            //if (ingameUI == null)
            //{
            //    ingameUI = GameObject.Find("UITopCanvas").transform.Find("IngameUI").GetComponent<IngameTopUI>();
            // 
            //}
            //
            //ingameUI.Init();

            //if (stageManager == null)
            //{
            //    stageManager = this.GetComponent<StageManager>();
            //
            //}
            //
            //ingameUI.Init();

            if (playerControler == null)
            {
                playerControler = GameObject.Find("BattleManager").GetComponent<PlayerControler>();
            
            }
            playerControler.Init();
            
            if (cardSorting == null)
            {
                cardSorting = GameObject.Find("CardCanvas").transform.Find("Hand").GetComponent<CardSorting>();
            }
            cardSorting.Init();
            
            DontDestroyOnLoad(this);
            DontDestroyOnLoad(gameObject);

        }

        //private void Update()
        //{
        //    if(Input.GetKeyDown(KeyCode.T))
        //    {
        //        SceneManager.LoadScene("TestScence");
        //    }
        //}
    }
}
