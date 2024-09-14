using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerWin : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject boss;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (boss == null)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }

    }
}
