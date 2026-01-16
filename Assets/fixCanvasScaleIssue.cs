using UnityEngine;

public class fixCanvasScaleIssue : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        if(GetComponent<networkText>().enabled == false)
        {
            if(transform.parent.localScale.x != 1)
            {
                float scaleMultiplier = 1 / transform.parent.localScale.x;
                transform.localScale = new Vector3(transform.localScale.x * transform.parent.localScale.x, transform.localScale.y * transform.parent.localScale.y, transform.localScale.z * transform.parent.localScale.x);
                transform.parent.localScale = new Vector3(1, 1, 1);
                transform.position = new Vector3(transform.position.x * scaleMultiplier, transform.position.y * scaleMultiplier, transform.position.z * scaleMultiplier);
                
            }
            GetComponent<networkText>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
