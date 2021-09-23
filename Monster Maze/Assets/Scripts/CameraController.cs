using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10;
    GameObject cameraSetup;
    ///protected GameObject character;

    // Start is called before the first frame update
    void Start()
    {
        cameraSetup = GameObject.Find("CameraPos");
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
            cameraSetup.transform.position, speed * Time.deltaTime);
    }
}
