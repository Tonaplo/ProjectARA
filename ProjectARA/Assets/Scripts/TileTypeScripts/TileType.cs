using UnityEngine;
using System.Collections;

public class TileType : MonoBehaviour {

    public TypeOfTile Type;

    void Awake()
    { }

    public enum TypeOfTile
    {
        Empty,
        Laser,
        Mirror,
        Straight
    }
}
