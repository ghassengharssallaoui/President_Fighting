using UnityEngine;
using UnityEngine.UI;

public class canvasPainter : MonoBehaviour
{
     NetworkCanvasVariables myVariables; 
    public Text text;
    public bool attachedToCam;
    void OnEnable()
    {
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //textRect = text.GetComponent<RectTransform>();
        myVariables = GetComponent<NetworkCanvasVariables>();


    }

    // Update is called once per frame
    void Update()
    {
        if(attachedToCam == false)
        {
            transform.SetParent(GameObject.Find("Main Camera").transform);
            attachedToCam = true;
        }
        if (myVariables != null)
        {
            gameObject.transform.position = myVariables.canvasPosition;
            gameObject.transform.localScale = myVariables.canvasScale;
            text.gameObject.transform.position = myVariables.textPosition;
            text.GetComponent<RectTransform>().sizeDelta = myVariables.textRect;
            text.gameObject.transform.localScale = myVariables.textScale;
            text.color = myVariables.textColor;
            text.text = myVariables.textContent;

        }
        else
        {
            text.text = "";
        }
    }
}
