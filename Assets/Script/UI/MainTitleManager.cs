using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class MainTitleManager : MonoBehaviour
{
    public GameObject[] cloudes;
    public Vector3[] cloudPositions;
    public CanvasGroup faidingCanvas;
    public GameObject saveBeforeObject;
    public Button[] buttons;
    [SerializeField] private Texture2D cursorImg;


    public void Start()
    {
        for(int i = 0; i <cloudes.Length; i++)
        {
            cloudPositions[i] = cloudes[i].transform.position;
        }

        for(int i = 0; i<buttons.Length;i++)
        {
            ChangeSize(buttons[i].gameObject);
        }

        cloudes[2].transform.DOMoveX(2800, 90);
        cloudes[3].transform.DOMoveX(2800, 90);
        cloudes[0].transform.DOMoveX(-870, 90);
        cloudes[1].transform.DOMoveX(-870, 90);
        cloudes[4].transform.DOMoveX(-870, 90);

        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.Auto);
    }


    void FixedUpdate()
    {
        if (cloudes[2].transform.position.x <= 2800)
        {
            cloudes[2].transform.position = cloudPositions[2];
            cloudes[2].transform.DOMoveX(2800, 90);
        }

        if (cloudes[4].transform.position.x <= -870)
        {
            cloudes[4].transform.position = cloudPositions[4];
            cloudes[4].transform.DOMoveX(-870, 90);
        }
        
        
    }

    public void StartButton()
    {
        FadeIn();
       
    }

    public void FadeIn()
    {
        faidingCanvas.GetComponent<Image>().raycastTarget = true;
        float fadeDuration = 1;
        float fadeAmount =1;
        faidingCanvas.alpha = 0;
        faidingCanvas.DOFade(fadeAmount, fadeDuration).OnComplete(
             () => SceneManager.LoadSceneAsync("GameScence"));
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ChangeSize(GameObject Obj)
    {
        EventTrigger eventTrigger = Obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerEnter = new EventTrigger.Entry();
        entry_PointerEnter.eventID = EventTriggerType.PointerEnter;
        entry_PointerEnter.callback.AddListener((data) => { OnPointerEnter((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerEnter);

        EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();
        entry_PointerExit.eventID = EventTriggerType.PointerExit;
        entry_PointerExit.callback.AddListener((data) => { OnPointerExit(); });
        eventTrigger.triggers.Add(entry_PointerExit);

    }

    public void OnPointerEnter(PointerEventData data)
    {
        saveBeforeObject = data.pointerEnter.gameObject;
        data.pointerEnter.transform.DOScale(4, 0.5f);
    }

    public void OnPointerExit()
    {
        saveBeforeObject.transform.DOScale(3, 0.5f);

    }


}
