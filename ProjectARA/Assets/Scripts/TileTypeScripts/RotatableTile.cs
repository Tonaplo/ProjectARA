using UnityEngine;
using System.Collections;

public class RotatableTile : MonoBehaviour {

    void OnMouseDown()
    {
        transform.Rotate(Vector3.forward * -90f);
    }
}
