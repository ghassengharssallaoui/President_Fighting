using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noParent : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        transform.parent = null;
    }
}
