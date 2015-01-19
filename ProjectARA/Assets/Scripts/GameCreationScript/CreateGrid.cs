using UnityEngine;
using System.Collections;

public class CreateGrid : MonoBehaviour {

    public Transform Tile;
    public Transform Emitter;

	// Use this for initialization
	void Start () 
    {
	    float tileWidth = (float)Tile.renderer.bounds.size.x;

        for (int y = -4; y < 5; y++) {
            for (int x = -4; x < 5; x++) {
                Instantiate(Tile, new Vector3(x * tileWidth, y * tileWidth, 0), Quaternion.identity);
            }
        }

        Emitter.GetComponent<FireLaser>().direction = FireLaser.Direction.Right;
        Instantiate(Emitter, new Vector3(-5 * tileWidth, 0 * tileWidth, 0), Quaternion.Euler(Vector3.forward * -90f));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
