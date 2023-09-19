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
        public IngameTopUI ingameTopUI;

        public StageManager stageManager;
        public PlayerControler playerControler;
        public CardManager cardManager;
        public PotionManager potionManager;
        public BattleManager battleManager;
        public InGameUIManager inGameUIManager;

        public MapManager mapManager;

        public Initializer initilizer;

        public SpawnManager spawnManager;


        //void OnEnable()
        //{
        //    // 델리게이트 체인 추가
        //    SceneManager.sceneLoaded += OnSceneLoaded;
        //}

        //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        //{
        //    Debug.Log("씬 교체됨, 현재 씬: " + scene.name);



        //    if (scene.name == "GameScence")
        //    {
        //        Init();
        //    }
        //    // 씬 전환 효과 (Fade In)

        //}

        private void Start()
        {
            StartCoroutine(Init());
        }

        public IEnumerator Init()
        {
            if (dataManager == null)
            {
                dataManager = new DataManager();
            }

            dataManager.Init();

           yield return new WaitUntil(() => dataManager.ReadyData() == true);


            //---------------------------------------DataManager 무조건 선순위----------------------------------------------------------
            if(mapManager == null)
            {
                mapManager = GameObject.Find("MapPopup").transform.GetComponent<MapManager>();
            }

            mapManager.Init();
            mapManager.FadeOut();

            if (soundManager == null)
            {
                soundManager = GameObject.Find("SoundManager").GetComponent<Soundmanager>();
            }

            soundManager.ChangeBGM();

            if (ingameTopUI == null)
            {
                ingameTopUI = GameObject.Find("UITopCanvas").transform.Find("IngameTopUI").GetComponent<IngameTopUI>();
             
            }
            
            ingameTopUI.Init();


            if (stageManager == null)
            {
                stageManager = this.GetComponent<StageManager>();

            }

            if (potionManager == null)
            {
                potionManager = GameObject.Find("UITopCanvas").transform.Find("IngameTopUI").GetComponent<PotionManager>();
            }


            GameObject ingameUi = GameObject.Find("InGameUiCanvas");

            if (playerControler == null)
            {
                playerControler = ingameUi.GetComponent<PlayerControler>();
            }

            playerControler.Init();

            if (inGameUIManager == null)
            {
                inGameUIManager = ingameUi.GetComponent<InGameUIManager>();
            }
            inGameUIManager.Init();

            if (battleManager == null)
            {
                battleManager = GetComponent<BattleManager>();
            }

            if (cardManager == null)
            {
                cardManager = ingameUi.transform.Find("Card").transform.Find("Hand").GetComponent<CardManager>();
            }

            cardManager.Init();

            if (spawnManager == null)
            {
                spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
            }

            

        }

        public void LoadMainTitle()
        {
            SceneManager.LoadSceneAsync("MainTitle");
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
