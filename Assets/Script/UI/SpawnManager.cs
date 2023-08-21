using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameWork
{
    public enum Stage
    {
        NormalMonster,
        NamedMonster,
        BossMonster
    }

    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] GameObject[] arrNormarMonsters;
        [SerializeField] GameObject namedMonster;
        [SerializeField] GameObject bossMonster;

        public void Init()
        {
            SpawnMonster(Stage.NormalMonster);
        }

        private void SpawnMonster(Stage enemyStage)
        {
            List<GameObject> listSpawnMonster = new List<GameObject>();

            switch(enemyStage)
            {
                case Stage.NormalMonster:
                    int unitCount = Random.Range(1, 4);
                    for(int i = 0; i < unitCount; i++)
                        listSpawnMonster.Add(arrNormarMonsters[Random.Range(0, arrNormarMonsters.Length)]);
                    break;
                case Stage.NamedMonster:
                    for (int i = 0; i < 3; i++)
                        listSpawnMonster.Add(namedMonster);
                    break;
                case Stage.BossMonster:
                    listSpawnMonster.Add(bossMonster);
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

            for (int i = 0; i < listSpawnMonster.Count; i++)
            {
                GameObject characterParent = Instantiate(listSpawnMonster[i], Vector2.zero, Quaternion.identity, this.transform);
                CharacterBase character = characterParent.transform.GetChild(0).GetComponent<CharacterBase>();
                Vector3 pos = Camera.main.WorldToViewportPoint(characterParent.transform.position);
                pos.x = spawnPosX[i];
                pos.y = 0.5f;
                pos = Camera.main.ViewportToWorldPoint(pos);
                characterParent.transform.position = character.charaterPos = pos;
                GameManager.Instance.battleManager.enemyCharacters.Add(character);
            }
        }
    }
}