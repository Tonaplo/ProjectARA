using UnityEngine;
using System.Collections;

public class CreateGrid : MonoBehaviour {

    public Transform PrismTiles;
    public Transform StraightTiles;
    public Transform FilterTiles;
    public Transform Emitter;
    public Transform Receiver;
    public Transform Wall;

    int xcoord, ycoord;
    float tileWidth;

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

        tileWidth = (float)PrismTiles.renderer.bounds.size.x;

        xcoord = Random.Range(-4, 5);
        ycoord = Random.Range(-4, 5);

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

        GenerateEmitterAndReceiver();
    }

    void GenerateEmitterAndReceiver()
    {
        Quaternion emitterRotation;

        if (Random.value < 0.5f)
        {
            xcoord = Random.Range(-4, 5);
            if (Random.value < 0.5f)
            {
                ycoord = -5;
                Emitter.GetComponent<FireLaser>().direction = FireLaser.Direction.Right;
            }
            else
            {
                ycoord = 5;
                Emitter.GetComponent<FireLaser>().direction = FireLaser.Direction.Left;
            }
            emitterRotation = Quaternion.Euler(Vector3.forward * 90f);
        }
        else
        {
            ycoord = Random.Range(-4, 5);
            if (Random.value < 0.5f)
            {
                Emitter.transform.Rotate(Vector3.forward * -90f);
                xcoord = -5;
                Emitter.GetComponent<FireLaser>().direction = FireLaser.Direction.Up;
            }
            else
            {
                Emitter.transform.Rotate(Vector3.forward * -90f);
                xcoord = 5;
                Emitter.GetComponent<FireLaser>().direction = FireLaser.Direction.Down;
            }
            emitterRotation = Quaternion.identity;
        }


        Instantiate(Emitter, new Vector3(ycoord * tileWidth, xcoord * tileWidth, 0), emitterRotation);
        Instantiate(Receiver, new Vector3(xcoord * tileWidth, ycoord * tileWidth, 0), Quaternion.identity);

        GameObject.FindGameObjectWithTag("Laser").GetComponent<FireLaser>().Fire();

        GenerateWallsAroundTiles();
    }

    void GenerateWallsAroundTiles()
    {
        for (int i = -5; i < 6; i++)
        {
            if ((xcoord == i && ycoord == -5) || (xcoord == -5 && ycoord == i))
                ;//Do nothing
            else
                Instantiate(Wall, new Vector3(-5 * tileWidth, i * tileWidth, 0), Quaternion.identity);
        }

        for (int i = -5; i < 6; i++)
        {
            if ((xcoord == i && ycoord == 5) || (xcoord == 5 && ycoord == i))
                ;//Do nothing
            else
                Instantiate(Wall, new Vector3(5 * tileWidth, i * tileWidth, 0), Quaternion.identity);
        }

        for (int i = -4; i < 5; i++)
        {
            if ((ycoord == i && xcoord == -5) || (ycoord == -5 && xcoord == i))
                ;//Do nothing
            else
                Instantiate(Wall, new Vector3(i * tileWidth, -5 * tileWidth, 0), Quaternion.identity);
        }

        for (int i = -4; i < 5; i++)
        {
            if ((ycoord == i && xcoord == 5) || (ycoord == 5 && xcoord == i))
                ;//Do nothing
            else
                Instantiate(Wall, new Vector3(i * tileWidth, 5 * tileWidth, 0), Quaternion.identity);
        }
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
