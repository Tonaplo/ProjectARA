using UnityEngine;
using System.Collections;

public class CreateGrid : MonoBehaviour {

    public Transform Tile;
	// Use this for initialization
	void Start () 
    {
	    float tileWidth = (float)Tile.renderer.bounds.size.x;
        for (int y = 0; y < 5; y++) {
            for (int x = 0; x < 5; x++) {
                Instantiate(Tile, new Vector3(x * tileWidth, y * tileWidth, 0), Quaternion.identity);
            }
        }
 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
