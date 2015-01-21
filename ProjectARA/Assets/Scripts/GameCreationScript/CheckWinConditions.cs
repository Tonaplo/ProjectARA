using UnityEngine;
using System.Collections;

public class CheckWinConditions : MonoBehaviour {

    GameObject[] receivers;
    public GameObject playAgainButton;

    void Awake()
    {
        receivers = GameObject.FindGameObjectsWithTag("Receiver");
    }

    public void DidWeWin()
    {
        for (int i = 0; i < receivers.Length; i++)
        {
            if (!receivers[i].GetComponent<Tile_Receiver>().isSatisfied())
                return;
        }

        Debug.Log("We won!");
        Instantiate(playAgainButton);
    }
}
