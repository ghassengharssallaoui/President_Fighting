using UnityEngine;
using UnityEngine.UI;

public class networkText : MonoBehaviour
{
    spriteGod god;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        god = GameObject.Find("Sprite God").GetComponent<spriteGod>();
    }
    // Update is called once per frame
    void Update()
    {

        bool alreadyInThere = false;
        for (int i = 0; i < god.textList.Length; i++)
        {
            if (god.textList[i] == GetComponent<networkTextVariables>())
            {
                alreadyInThere = true;
            }
        }

        for (int i = 0; i < god.textList.Length; i++)
        {
            if (alreadyInThere == false)
            {
                if (god.textList[i] == null)
                {
                    god.textList[i] = GetComponent<networkTextVariables>();
                    alreadyInThere = true;
                }
            }
        }
        
    }
    void OnDisable()
    {

        for (int i = 0; i < god.textList.Length; i++)
        {
            if (god.textList[i] == GetComponent<networkTextVariables>())
            {
                god.textList[i] = null;
            }
        }
        
    }
}