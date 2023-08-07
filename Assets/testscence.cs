using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class testscence : MonoBehaviour
{
    public GameObject Up;
    public GameObject Down;
    public Transform Canvas;
   
    // Start is called before the first frame update
    private void Start()
    {

        Debug.Log(Up.GetComponent<RectTransform>().anchoredPosition.x);
        Debug.Log(Down.transform.position.x);
        float x = Up.transform.position.x + Down.transform.position.x;
        float y = Up.transform.position.y + Down.transform.position.y;

       Vector3 offset = Up.transform.position - Down.transform.position;

       float deg = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

       float width = Vector2.Distance(Up.transform.position, Down.transform.position);
        //float angle = Vector2.Angle(Up.transform.position, Down.transform.position);

       GameObject obj = Instantiate(Up, new Vector2(x/2,y/2), Quaternion.identity,Canvas);
       obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width -150, obj.GetComponent<RectTransform>().sizeDelta.y);
       obj.transform.rotation = Quaternion.Euler(0, 0, deg);


    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        SceneManager.LoadScene("UITestScence");
    //    }
    //}


}
