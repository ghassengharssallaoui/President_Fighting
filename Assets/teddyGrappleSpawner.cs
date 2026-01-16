using UnityEngine;

public class teddyGrappleSpawner : MonoBehaviour
{
    public GameObject frame1;
    public GameObject frame2;
    public GameObject armPrefab;
    Vector2 vect;
    public bool rotated;
    bool spawned;
    PlayerInfo info;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject dummy;
        dummy = gameObject;
        while (dummy.GetComponent<PlayerInfo>() == null)
        {
            dummy = dummy.transform.parent.gameObject;
        }
        info = dummy.GetComponent<PlayerInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        vect = GetComponent<rememberInputVector>().vector;
        if (frame1.active == true && rotated == false)
        {
            float dummyFloat;
            if (info.transform.localScale.x < 0)
            {
                dummyFloat = 1;
            }
            else
            {
                dummyFloat = 0;
            }
            frame1.transform.parent.transform.eulerAngles = new Vector3(0, 0, 180 * dummyFloat + (Mathf.Atan2(vect.y, vect.x) * 57.2f));
            rotated = true;
        }
        if (frame1.active == false)
        {
            rotated = false;
        }
        if (frame2.active == true && spawned == false)
        {
            float dummyFloat;
            if (info.transform.localScale.x < 0)
            {
                dummyFloat = 0;
            }
            else
            {
                dummyFloat = 0;
            }
            GameObject dummy = Instantiate(armPrefab, frame2.transform.parent.position + new Vector3(0, 0, 0.001f), Quaternion.identity);
            dummy.transform.eulerAngles = new Vector3(0, 0, 180 * dummyFloat + (Mathf.Atan2(vect.y, vect.x) * 57.2f));
            dummy.GetComponent<TeddyGrappleMovement>().info = info;
            spawned = true;
        }
        if (frame2.active == false)
        {
            spawned = false;
        }
    }
}
