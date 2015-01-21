using UnityEngine;
using System.Collections;

public class PlayAgainScript : MonoBehaviour {

    GameObject gameController;
    bool reload = false;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        if(reload)
            Application.LoadLevel(Application.loadedLevel);
    }

    void OnMouseDown()
    {
        reload = true;
    }
}
