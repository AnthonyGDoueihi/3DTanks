using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour {

    AITankController[] enemyArray;
    public Text restart;
    public Text win;
    public Text lose;
    bool bGameEnd = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Use this for initialization
    void Start () {
        restart.enabled = false;
        win.enabled = false;
        lose.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        enemyArray = FindObjectsOfType<AITankController>();
        if (enemyArray.Length == 0)
        {
            YouWin();
        }

        if (bGameEnd)
        {
            restart.enabled = true;
            if (Input.GetButtonDown("Restart"))
            {
                SceneManager.LoadScene("Combat");

            }
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void YouWin()
    {
        win.enabled = true;
        bGameEnd = true;
    }

    public void YouLose()
    {
        lose.enabled = true;
        bGameEnd = true;

    }
}
