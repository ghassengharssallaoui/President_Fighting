using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class urlButton : MonoBehaviour
{
    public string link;
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OpenURL);
    }

    public void OpenURL()
    {
        Application.OpenURL(link);
    }

}