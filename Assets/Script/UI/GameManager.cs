using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    using FrameWork.Data;
    public class GameManager : MonoSingleton<GameManager>
    {

        public static DataManager dataManager { get; private set; }
        public static Soundmanager soundManager { get; private set; }
        public static IngameTopUI ingameUI { get; private set; }
        public PlayerControler playerControler { get; private set; }
        public CardSorting cardSorting { get; private set; }

        [SerializeField] Initializer initilizer;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
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

            if (dataManager == null)
            {
                dataManager = new DataManager();
            }
            dataManager.Init();

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
        }
    }
}
