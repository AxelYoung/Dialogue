using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Restart();
    }

    public void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            print(SceneManager.GetActiveScene().buildIndex + "");
        }
    }
}
