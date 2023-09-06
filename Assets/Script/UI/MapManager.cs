using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.ResourceManagement;
using UnityEngine.UI;
using DG.Tweening;
namespace FrameWork
{
    
    public enum MapField
    {
        Event,
        Monster,
        EliteMonster,
        Shop,
        Sleep,
        Treasure,
        Boss
    }


    
    public class mapNode
    {
        public List<mapNode> listAdjacent = new List<mapNode>();//연결링크
        public bool marked;
        public MapField field;
        public int myIndex;
        public int myFloor;
        public int myPosition;

        public void addEdge(mapNode i1, mapNode i2)
        {
            if (!i1.listAdjacent.Contains(i2))
            {
                i1.listAdjacent.Add(i2);
            }

            if (!i2.listAdjacent.Contains(i1))
            {
                i2.listAdjacent.Add(i1);
            }

        }

        public mapNode FindLink(mapNode mapNode, int myPosition, List<mapNode> listFloor)
        {

           // Debug.Log($"----my node Floor : {mapNode.myFloor } / Position : {mapNode.myPosition} = = {myPosition}, target count {listFloor.Count}");

            for (int i = 0; i < listFloor.Count; i++)
            {
              //  Debug.Log($"  [{i}] listFloorPosition {listFloor[i].myPosition}");

                if (myPosition == listFloor[i].myPosition)
                {
                  //  Debug.Log("====== end");
                    return listFloor[i];
                }

            }

            int increase = 1;

            for (int i = 0; i < listFloor.Count;)
            {
               // Debug.Log($" increase {increase} => target {listFloor[i].myPosition} ({i}/{listFloor.Count}) - {myPosition - increase} || + {myPosition + increase }");

                if (myPosition - increase == listFloor[i].myPosition)
                {
                    return listFloor[i];
                }

                else if (myPosition + increase == listFloor[i].myPosition)
                {
                    return listFloor[i];

                }

                //Debug.Log($"i {i} - count {listFloor.Count} ||| next {i < listFloor.Count - 1} reset {i == listFloor.Count - 1}" +"여기" );
                if (i <listFloor.Count -1)
                {
                    i++;
                }

                else if (i == listFloor.Count - 1)
                {
                    increase++;
                    i = 0;
                }
            }

            return null;
        }

    }

    public class MapManager : MonoBehaviour
    {
        public Transform mapObject;
        public List<mapNode> listMapGraph = new List<mapNode>();
        public GameObject node;
        public List<GameObject> listNodeZones;
        public List<GameObject> listNodes;
        public GameObject objectLine;
        public GameObject nodeCircle;
        public Sprite[] nodeImages;
        public static MapField fieldInfo;
        public int nowIndex = 999;        
        public CanvasGroup fadingCanvasGroup;
        public void Init()
        {
           
            for (int i = 0; i < mapObject.childCount; i++)
            {
                listNodeZones.Add(mapObject.GetChild(i).gameObject);
            }
            
            SettingNode();
           // Debug.LogError(FrameWork.Data.DataManager.data.eventData.GetEventData()[0].effectText);
        }

        public void Start()
        {
            Init();
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(listMapGraph);
            File.WriteAllText(Application.dataPath + "/DataJson"+"/testMap.json", json);
        }

        public void SettingNode()
        {
            int maxYsize = 30;
            int minYsize = -30;
            //string objectPath = "Assets/Prefabs/UI/MapNode.prefab";
            int[] arrNodeXPosition = { 100, 0, -100, -200, -300, -400 };
            ///////////////////////////// 보스 노드 오브젝트 넣어주기
            listNodes.Add(listNodeZones[0].transform.GetChild(0).gameObject);
            listMapGraph.Add(new mapNode());
            listMapGraph[listMapGraph.Count - 1].myFloor = 0;
            ///////////////////////////// 보스 노드 오브젝트 넣어주기 끝
            for (int i = 1; i < listNodeZones.Count; i++)//일반 노드 오브젝트 생성 
            {
                Transform parent = listNodeZones[i].GetComponent<Transform>();
                int randomNodeSize = Random.Range(1, 6);
                
                for (int z = 0; z < randomNodeSize; z++)
                {
                    float randomY = Random.Range(minYsize, maxYsize);
                    GameObject obj = Instantiate(node, new Vector2(parent.position.x + arrNodeXPosition[randomNodeSize] + Random.Range(-30, 30) + z * 200
                         , parent.position.y + randomY),Quaternion.identity, parent);

                    listNodes.Add(obj);
                    listMapGraph.Add(new mapNode());
                    listMapGraph[listMapGraph.Count - 1].myFloor = i;
                    listMapGraph[listMapGraph.Count - 1].myPosition = z;
                    listMapGraph[listMapGraph.Count - 1].myIndex = listMapGraph.Count - 1;
                    listMapGraph[listMapGraph.Count - 1].field = ((MapField)Random.Range(0, 5));

                    if(listMapGraph[listMapGraph.Count - 1].field == MapField.EliteMonster)//엘리트 확률 줄이기
                    listMapGraph[listMapGraph.Count - 1].field = ((MapField)Random.Range(0, 5));

                    if(listMapGraph[listMapGraph.Count - 1].myFloor == 10)//첫턴은 무조건 몬스터
                        listMapGraph[listMapGraph.Count - 1].field = MapField.Monster;

                    if (listMapGraph[listMapGraph.Count - 1].myFloor == 1)// 보스 전은 무조건 휴식
                        listMapGraph[listMapGraph.Count - 1].field = MapField.Sleep;

                    int Index = listMapGraph.Count - 1;
                    obj.GetComponent<Button>().onClick.AddListener(() => ClickNodeButton(Index));
                    
                }

            }

            linkNodes();
            SettingNodeButton();

        }

        public void linkNodes()
        {
            for (int i = 0; i < listMapGraph.Count; i++)
            {
                    List<mapNode> listUnderFloor = new List<mapNode>();
                    List<mapNode> listUpperFloor = new List<mapNode>();

                    for (int z = i - 1; z > -1; z--) // 왼쪽부터 위로
                    {
                        if (listMapGraph[i].myFloor -1 == listMapGraph[z].myFloor)
                        {
                            listUpperFloor.Add(listMapGraph[z]);
                        }
                    }

                    var linkTargetUpObject = listMapGraph[i].FindLink(listMapGraph[i], listMapGraph[i].myPosition, listUpperFloor);
                  
                    if (linkTargetUpObject != null)
                        listMapGraph[i].addEdge(listMapGraph[i], linkTargetUpObject);

                    for (int z = i + 1; z < listMapGraph.Count; z++)//우측부터 밑으로 
                    {
                        if (listMapGraph[i].myFloor + 1 == listMapGraph[z].myFloor )
                        {
                            listUnderFloor.Add(listMapGraph[z]);
                        }
                    }

                    var linkTargetDownObject = listMapGraph[i].FindLink(listMapGraph[i],listMapGraph[i].myPosition, listUnderFloor);

                    if (linkTargetDownObject != null)
                        listMapGraph[i].addEdge(listMapGraph[i], linkTargetDownObject);

            }

            
            DrawLinkLine();
        }

        public void DrawLinkLine()
        {
           
            for(int i = listMapGraph.Count -1; i > -1; i--)
            {
                List<int> listLinkIndex = new List<int>();

                for (int z = 0; z < listMapGraph[i].listAdjacent.Count; z++)
                {   
                    listLinkIndex.Add(listMapGraph[i].listAdjacent[z].myIndex);
                }

                Transform parent = listNodes[i].GetComponent<Transform>().transform.parent;

                for (int z = 0; z < listLinkIndex.Count; z++)
                {
                    Vector2 standardNode =  listNodes[i].transform.position;
                    Vector2 targetNode = listNodes[listLinkIndex[z]].transform.position;

                    float x = standardNode.x + targetNode.x;
                    float y = standardNode.y + targetNode.y;

                    Vector3 offset = standardNode - targetNode;

                    float deg = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
                    float width = Vector2.Distance(standardNode, targetNode);
                    //float angle = Vector2.Angle(Up.transform.position, Down.transform.position);

                    GameObject obj = Instantiate(objectLine, new Vector2(x / 2, y / 2), Quaternion.Euler(0, 0, deg), parent);
                    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width - 85, obj.GetComponent<RectTransform>().sizeDelta.y);
                    
                }
               
            }
        }

        public void ClickNodeButton(int index)
        {
            for (int i = 0; i < listMapGraph.Count; i++)
            {
                if (listMapGraph[index].myFloor == listMapGraph[i].myFloor)
                {
                    listNodes[i].GetComponent<Button>().interactable = false;
                }

            }

            if (index != 0)
            {
                Animator objCircle = Instantiate(nodeCircle, listNodes[index].transform.position,
                    Quaternion.identity, listNodes[index].transform.parent).GetComponent<Animator>();
                objCircle.SetBool("DrowCircle", true);
            }

            listMapGraph[index].marked = true;
            listNodes[index].GetComponent<Button>().enabled = false;
            nowIndex = index;

            fieldInfo = listMapGraph[index].field;

            StartCoroutine(WaitDrowCircle());
            
            
        }

        IEnumerator WaitDrowCircle()
        {
            yield return new WaitForSeconds(0.45f);

            FadeOut();
            GameManager.Instance.stageManager.ControlField(fieldInfo);

            for (int i = 0; i < listMapGraph[nowIndex].listAdjacent.Count; i++)
            {
                  if(listMapGraph[nowIndex].myFloor -1 ==  listMapGraph[nowIndex].listAdjacent[i].myFloor)
                  {
                      if(listMapGraph[nowIndex].listAdjacent[i].myFloor != 0)
                      listNodes[listMapGraph[nowIndex].listAdjacent[i].myIndex].GetComponent<Button>().interactable = true;
                  }
            }

            this.transform.parent.gameObject.SetActive(false);
           
        }

        public void SettingNodeButton()
        {
          
            if(nowIndex == 999)//저장되어있는 노드가 없다면 999임 즉 새로시작이라면
            {
               for(int i = 0; i<listMapGraph.Count; i++)
               {
                    if(listMapGraph[i].myFloor == 10)
                    {
                        listNodes[i].GetComponent<Button>().interactable = true;
                    }
               }
            }

           for (int i = 0; i <listNodes.Count;i++)
           {
                
                if(listMapGraph[i].marked == true)
                {
                    listNodes[i].GetComponent<Button>().enabled = false;

                    Instantiate(nodeCircle, listNodes[i].transform.position, 
                    Quaternion.identity, listNodes[i].transform.parent);
                }

                if( i >= 1)
                listNodes[i].GetComponent<Image>().sprite = nodeImages[(int)listMapGraph[i].field];
           }

            if (nowIndex != 999)
            {
                for (int i = 0; i < listMapGraph[nowIndex].listAdjacent.Count; i++)
                {

                    if (listMapGraph[nowIndex].myFloor - 1 == listMapGraph[nowIndex].listAdjacent[i].myFloor)
                    {
                        int index = listMapGraph[nowIndex].listAdjacent[i].myIndex;
                        listNodes[index].GetComponent<Button>().interactable = true;
                    }     
                }
            }
        }

        public void FadeOut()
        {
            float fadeDuration = 1;
            float fadeAmount = 0;
            fadingCanvasGroup.alpha = 1;
            fadingCanvasGroup.gameObject.SetActive(true);
            fadingCanvasGroup.DOFade(fadeAmount, fadeDuration).OnComplete(
             () => fadingCanvasGroup.gameObject.SetActive(false));
        }

       
    }
}
