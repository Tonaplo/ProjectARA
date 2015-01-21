using UnityEngine;
using System.Collections;

public class TileType : MonoBehaviour {

    public TypeOfTile Type;

    void Awake()
    { }

    public enum TypeOfTile
    {
        Empty,
        Emitter,
        Receiver,
        Prism,
        Straight,
        Filter,
        Wall
    }
}
