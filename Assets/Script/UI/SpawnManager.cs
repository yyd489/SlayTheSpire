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

        public void Init(MapField enemyStage)
        {
            SpawnMonster(enemyStage);
        }

        private void SpawnMonster(MapField enemyStage)
        {
            List<GameObject> listSpawnMonster = new List<GameObject>();
            List<int> listSummonMonstetIndex = new List<int>();

            switch(enemyStage)
            {
                case MapField.Monster:
                    int unitCount = Random.Range(1, 4);
                    for (int i = 0; i < unitCount; i++)
                    {
                        int monsterIndex = Random.Range(0, arrNormarMonsters.Length);
                        listSpawnMonster.Add(arrNormarMonsters[monsterIndex]);
                        listSummonMonstetIndex.Add(monsterIndex);
                    }
                    break;
                case MapField.EliteMonster:
                    for (int i = 0; i < 2; i++)
                    {
                        listSpawnMonster.Add(namedMonster);
                        listSummonMonstetIndex.Add(5);
                    }
                    break;
                case MapField.Boss:
                    listSpawnMonster.Add(bossMonster);
                    listSummonMonstetIndex.Add(6);
                    break;
            }

            float[] spawnPosX = new float[listSpawnMonster.Count];

            if (listSpawnMonster.Count == 1) spawnPosX[0] = 0.75f;
            else if(listSpawnMonster.Count == 2)
            {
                spawnPosX[0] = 0.65f;
                spawnPosX[1] = 0.85f;
            }
            else
            {
                spawnPosX[0] = 0.6f;
                spawnPosX[1] = 0.75f;
                spawnPosX[2] = 0.9f;
            }


            var characterStat = GameManager.Instance.dataManager.data.monsterData.monsterData.listMonsterData;

            for (int i = 0; i < listSpawnMonster.Count; i++)
            {
                GameObject characterParent = Instantiate(listSpawnMonster[i], Vector2.zero, Quaternion.identity, this.transform);
                CharacterBase character = characterParent.transform.GetChild(0).GetComponent<CharacterBase>();
                Vector3 pos = Camera.main.WorldToViewportPoint(characterParent.transform.position);
                pos.x = spawnPosX[i];
                pos.y = 0.57f;
                pos = Camera.main.ViewportToWorldPoint(pos);
                characterParent.transform.position = pos;
                character.charaterPos = character.transform.localPosition;
                character.Init(characterStat[listSummonMonstetIndex[i]]);
                GameManager.Instance.battleManager.enemyCharacters.Add(character);
            }
        }

    }
}