using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrameWork
{
    public class Soundmanager :MonoBehaviour
    {
        public AudioSource backgroundAudio;
        public AudioSource effectAudio;
        public AudioSource effectAudio2;//이펙트 사운드 하나로 부족 할 시 보조 사운드
                                        //예) 버튼클릭 or 몬스터 공격이 소리가 겹칠수 있게하기위함 

        public AudioClip[] backgroundSound;

        public AudioClip[] uiEffectSound;

        public AudioClip[] battleEffectSound;

        public static Soundmanager instance;

        void OnEnable()
        {
            // 델리게이트 체인 추가
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ChangeBGM();
        }


        private void Awake()
        {
            Init();

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void Init()
        {
            if(backgroundAudio == null)
            {
                backgroundAudio = this.transform.Find("BGM").GetComponent<AudioSource>();
            }

            if(effectAudio == null)
            {
              effectAudio = this.transform.Find("Effect").GetComponent<AudioSource>();
            }

            if (effectAudio2 == null)
            {
                effectAudio2 = this.transform.Find("Effect2").GetComponent<AudioSource>();
            }

            ChangeBGM();
            DontDestroyOnLoad(this.gameObject);
        }

        public void ChangeBGM()
        {
            if(SceneManager.GetActiveScene().name == "MainTitle")
            {
                backgroundAudio.clip = backgroundSound[0];
                backgroundAudio.Play();
            }

            else if(SceneManager.GetActiveScene().name == "GameScence")
            {
                backgroundAudio.clip = backgroundSound[1];
                backgroundAudio.Play();
            }
        }

        public void effectPlaySound(int index)
        {
            effectAudio.clip = uiEffectSound[index];
            effectAudio.Play();
        }

        public void playBattleEffectSound(int index)
        {
            effectAudio2.clip = battleEffectSound[index];
            effectAudio2.Play();
        }
    }
}
