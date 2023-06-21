using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    using FrameWork.Data;
    public class GameManager : MonoSingleton<GameManager>
    {
        public static DataManager dataManager { get; private set; }

    }
}
