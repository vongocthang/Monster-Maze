using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject item;
    public GameObject up;
    public GameObject down;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = down.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if(Vector3.Distance(item.transform.position, up.transform.position) == 0)
        {
            target = down.transform.position;
        }
        if(Vector3.Distance(item.transform.position, down.transform.position) == 0)
        {
            target = up.transform.position;
        }

        item.transform.position = Vector3.MoveTowards(item.transform.position, target, 1*Time.deltaTime);
        item.transform.Rotate((new Vector3(0, 1, 0)) * 50 * Time.deltaTime);
    }
}
