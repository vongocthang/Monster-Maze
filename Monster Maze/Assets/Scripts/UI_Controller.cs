using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Controller : MonoBehaviour
{
    PlayerController playerCon;
    JoystickController joyCon;
    public GameObject attack;
    public GameObject joystick;
    //Game over
    public Button home1;
    public Button reset1;
    public Button revival;
    //Complete
    public Button home2;
    public Button reset2;
    public Button next;
    //
    public GameObject gameOver;
    public GameObject complete;

    // Start is called before the first frame update
    void Start()
    {
        playerCon = GameObject.Find("Player").GetComponent<PlayerController>();
        joyCon = GameObject.Find("Player").GetComponent<JoystickController>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator GameOver()
    {
        if (playerCon.dead == true)
        {
            attack.SetActive(false);
            joystick.SetActive(false);
            //playerCon.enabled = false;
            joyCon.enabled = false;

            yield return new WaitForSeconds(3);

            gameOver.SetActive(true);
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
