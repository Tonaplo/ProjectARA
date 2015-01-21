using UnityEngine;
using System.Collections;

public class PlayAgainScript : MonoBehaviour {

    bool reload = false;

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
