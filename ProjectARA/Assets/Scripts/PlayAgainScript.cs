using UnityEngine;
using System.Collections;

public class PlayAgainScript : MonoBehaviour {

    GameObject gameController;

    void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void OnMouseDown()
    {
        gameController.GetComponent<CreateGrid>().GenerateNewGrid();
        Destroy(gameObject);
    }
}
