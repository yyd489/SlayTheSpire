using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FrameWork
{
    public class Soundmanager
    {
        public AudioSource backgroundAudio { get; private set; }
        public AudioSource effectAudio { get; private set; }

        public void Init(Initializer initializer)
        {
            backgroundAudio = initializer.backgroundAudio;
            effectAudio = initializer.effectAudio;
        }



    }
}
