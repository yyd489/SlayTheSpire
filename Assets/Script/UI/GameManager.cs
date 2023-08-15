using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
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
        public CardManager cardManager { get; private set; }
        public BattleManager battleManager { get; private set; }

        [SerializeField] Initializer initilizer;

       

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
              dataManager.data.cardData.cardCollect != null   && dataManager.data.relicsData.relicData != null
              && dataManager.data.eventData.eventData != null);

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

            //GameObject ingameUi = GameObject.Find("InGameUiCanvas");
            //
            //if (playerControler == null)
            //{
            //    playerControler = ingameUi.GetComponent<PlayerControler>();
            //
            //}
            //playerControler.Init();
            //
            //if (battleManager == null)
            //{
            //    battleManager = ingameUi.GetComponent<BattleManager>();
            //
            //}
            //playerControler.Init();
            //
            //if (cardManager == null)
            //{
            //    cardManager = ingameUI.transform.Find("Card").transform.Find("Hand").GetComponent<CardManager>();
            //}
            //cardManager.Init();
            //cardSorting.Init();

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
