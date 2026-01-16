using UnityEngine;

public class networkSprite : MonoBehaviour
{
    spriteGod god;
    public bool relativeToCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        god = GameObject.Find("Sprite God").GetComponent<spriteGod>();
        GameObject dummy = gameObject;
        while(dummy.transform.parent != null)
        {
            if(dummy.GetComponent<uiScaleWithAspectRatio>() == null)
            {
                //
            }
            else
            {
                relativeToCam = true;
            }
            dummy = dummy.transform.parent.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool alreadyInThere = false;
        if (relativeToCam == false)
        {
            for (int i = 0; i < god.rendererList.Length; i++)
            {
                if (god.rendererList[i] == GetComponent<SpriteRenderer>())
                {
                    alreadyInThere = true;
                }
            }

            for (int i = 0; i < god.rendererList.Length; i++)
            {
                if (alreadyInThere == false)
                {
                    if (god.rendererList[i] == null)
                    {
                        god.rendererList[i] = GetComponent<SpriteRenderer>();
                        alreadyInThere = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < god.uiList.Length; i++)
            {
                if (god.uiList[i] == GetComponent<SpriteRenderer>())
                {
                    alreadyInThere = true;
                }
            }

            for (int i = 0; i < god.uiList.Length; i++)
            {
                if (alreadyInThere == false)
                {
                    if (god.uiList[i] == null)
                    {
                        god.uiList[i] = GetComponent<SpriteRenderer>();
                        alreadyInThere = true;
                    }
                }
            }
        }
    }
    void OnDisable()
    {
        if (relativeToCam == false)
        {
            for (int i = 0; i < god.rendererList.Length; i++)
            {
                if (god.rendererList[i] == GetComponent<SpriteRenderer>())
                {
                    god.rendererList[i] = null;
                }
            }
        }
        else
        {
            for (int i = 0; i < god.uiList.Length; i++)
            {
                if (god.uiList[i] == GetComponent<SpriteRenderer>())
                {
                    god.uiList[i] = null;
                }
            }
        }
    }
}
