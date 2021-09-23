using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    FixedJoystick fixedJoystick;
    Rigidbody rigibody;
    public float speed = 10;
    GameObject character;
    Animator animator;
    AudioSource runAudio;
    AudioSource walkAudio;

    // Start is called before the first frame update
    void Start()
    {
        rigibody = gameObject.GetComponent<Rigidbody>();
        fixedJoystick = GameObject.FindObjectOfType<FixedJoystick>();
        character = GameObject.Find("Character");

        //animator = GameObject.Find("Human").GetComponent<Animator>();
        animator = GameObject.Find("Main").GetComponent<Animator>();
        runAudio = GameObject.Find("Player/Audio/Run").GetComponent<AudioSource>();
        walkAudio = GameObject.Find("Player/Audio/Walk").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Controller();
    }

    void Controller()
    {
        Vector3 direction = Vector3.forward * fixedJoystick.Vertical
            + Vector3.right * fixedJoystick.Horizontal;
        rigibody.AddForce(direction * speed * Time.deltaTime, ForceMode.VelocityChange);

        float distance = Vector3.Distance(direction, new Vector3(0, 0, 0));
        if(distance > 0 && distance < 0.8)
        {
            animator.PlayInFixedTime("Walk");
            walkAudio.enabled = true;
            runAudio.enabled = false;
        }
        if(distance >= 0.8 && distance <= 1)
        {
            animator.PlayInFixedTime("Run");
            runAudio.enabled = true;
            walkAudio.enabled = false;
        }
        if (distance == 0)
        {
            walkAudio.enabled = false;
            runAudio.enabled = false;
        }
        //Quaternion quay Player theo hướng Joystick
        if (distance > 0)
        {
            Vector3 a = direction + direction + direction + direction + direction;
            Vector3 relativePos = (a + gameObject.transform.position + new Vector3(0, .1f, 0))
            - character.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            Quaternion current = character.transform.localRotation;

            character.transform.localRotation = Quaternion.Slerp(current, rotation, 7 * Time.deltaTime);
        }
    }
}
