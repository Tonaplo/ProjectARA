﻿using UnityEngine;
using System.Collections;

public class CreateGrid : MonoBehaviour {

    public Transform PrismTiles;
    public Transform StraightTiles;
    public Transform FilterTiles;
    public Transform Emitter;
    public Transform Receiver;

	// Use this for initialization
	void Start () 
    {
        GenerateNewGrid();
	}

    public void GenerateNewGrid()
    {
        KillAllTiles();
        KillAllReceivers();
        KillAllLasers();

        float tileWidth = (float)PrismTiles.renderer.bounds.size.x;

        int xcoord = Random.Range(-4, 5);
        int ycoord = Random.Range(-4, 5);

        for (int y = -4; y < 5; y++)
        {
            for (int x = -4; x < 5; x++)
            {
                if (xcoord == x && ycoord == y)
                    Instantiate(FilterTiles, new Vector3(x * tileWidth, y * tileWidth, 0), Quaternion.identity);

                else if (Random.value < 0.5)
                    Instantiate(PrismTiles, new Vector3(x * tileWidth, y * tileWidth, 0), Quaternion.identity);

                else
                    Instantiate(StraightTiles, new Vector3(x * tileWidth, y * tileWidth, 0), Quaternion.identity);
            }
        }

        Emitter.GetComponent<FireLaser>().direction = FireLaser.Direction.Right;
        Instantiate(Emitter, new Vector3(-5 * tileWidth, 0 * tileWidth, 0), Quaternion.Euler(Vector3.forward * -90f));

        Instantiate(Receiver, new Vector3(5 * tileWidth, 3 * tileWidth, 0), Quaternion.identity);

       GameObject.FindGameObjectWithTag("Laser").GetComponent<FireLaser>().Fire();

    }

    void KillAllTiles()
    {
        GameObject[]  AllTiles = GameObject.FindGameObjectsWithTag("Tile");

        for (var i = 0; i < AllTiles.Length; i++)
        {
            Destroy(AllTiles[i]);
        }
    }

    void KillAllReceivers()
    {
        GameObject[] AllReceivers = GameObject.FindGameObjectsWithTag("Receiver");

        for (var i = 0; i < AllReceivers.Length; i++)
        {
            Destroy(AllReceivers[i]);
        }
    }

    void KillAllLasers()
    {
        GameObject[] AllLasers = GameObject.FindGameObjectsWithTag("Laser");

        for (var i = 0; i < AllLasers.Length; i++)
        {
            Destroy(AllLasers[i]);
        }
    }
}
