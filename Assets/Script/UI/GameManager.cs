using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
namespace FrameWork
{
    using FrameWork.Data;
    [System.Serializable]
    public class GameManager : MonoSingleton<GameManager>
    {

        public DataManager dataManager;
        public Soundmanager soundManager;
        public IngameTopUI ingameUI;

        public StageManager stageManager;
        public PlayerControler playerControler;
        public CardManager cardManager;
        public PotionManager potionManager;
        public BattleManager battleManager;

        public MapManager mapManager;

        public Initializer initilizer;

        public SpawnManager spawnManager;


        private void Start()
        {
            Init();
        }

        public async UniTaskVoid Init()
        {
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }

            dataManager.Init();

            await UniTask.WaitUntil(() => dataManager.data.characterData.characterInfoCollect != null &&
              dataManager.data.cardData.cardCollect != null   && dataManager.data.itemData.itemData != null
              && dataManager.data.eventData.eventData != null);


            //---------------------------------------DataManager 무조건 선순위----------------------------------------------------------
            if (soundManager == null)
            {
                soundManager = new Soundmanager();
            }
            
            soundManager.Init(initilizer);

            if (ingameUI == null)
            {
                ingameUI = GameObject.Find("UITopCanvas").transform.Find("IngameUI").GetComponent<IngameTopUI>();
             
            }
            
            ingameUI.Init();


            if (stageManager == null)
            {
                stageManager = this.GetComponent<StageManager>();

            }

            if (potionManager == null)
            {
                potionManager = GameObject.Find("UITopCanvas").transform.Find("IngameUI").GetComponent<PotionManager>();
            }
            potionManager.Init();

            GameObject ingameUi = GameObject.Find("InGameUiCanvas");

            if (playerControler == null)
            {
                playerControler = ingameUi.GetComponent<PlayerControler>();
            }
            playerControler.Init();

            if (battleManager == null)
            {
                battleManager = ingameUi.GetComponent<BattleManager>();
            }
            battleManager.Init();

            if (cardManager == null)
            {
                cardManager = ingameUi.transform.Find("Card").transform.Find("Hand").GetComponent<CardManager>();
            }

            if(spawnManager == null)
            {
                spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            }


            cardManager.Init();

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
