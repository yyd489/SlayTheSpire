using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject[] arrNormarMonsters;
        [SerializeField] GameObject namedMonster;
        [SerializeField] GameObject bossMonster;

        public void Init()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void SpawnUnit()
        {
            // 몬스터 6.5 7.5 8.5

            int unitCount = 3;
            float[] spawnPosX = new float[unitCount];

            if (unitCount == 1) spawnPosX[0] = 7.5f;
            else if(unitCount == 2)
            {
                spawnPosX[0] = 7f;
                spawnPosX[1] = 8f;
            }
            else
            {
                spawnPosX[0] = 6.5f;
                spawnPosX[1] = 7.5f;
                spawnPosX[2] = 8.5f;
            }

            for (int i = 0; i < unitCount; i++)
            {
                CharacterBase character = Instantiate(arrNormarMonsters[i], new Vector2(spawnPosX[i], 0f), Quaternion.identity, this.transform).GetComponent<CharacterBase>();
                GameManager.Instance.battleManager.enemyCharacters.Add(character);
            }
        }
    }
}

/*
   ironclad -4.65
   blueSlaver -2.95
   champ -6.15
   cultist -3.45
   fungiBeast -3.45
   jawWorm -3.95
   looter -3.35
   sentry -2.95
   */