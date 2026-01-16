using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bidenBlastExplode : MonoBehaviour
{
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 0)
        {
            GameObject dummy;
            transform.position = new Vector3(transform.position.x, 0, 0);
            dummy = Instantiate(explosion, transform.position, Quaternion.identity);
            if (transform.localScale.x > 1)
            {
                dummy.transform.localScale = transform.localScale;
            }
            Destroy(gameObject);
        }
    }
}
