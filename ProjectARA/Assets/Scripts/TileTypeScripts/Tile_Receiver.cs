using UnityEngine;
using System.Collections;

public class Tile_Receiver : MonoBehaviour {

    public Color inputColorNeeded;
    Color currentHittingColor;

    void Awake()
    {
        inputColorNeeded = Color.red;
        inputColorNeeded.a = 0.6f;
        currentHittingColor = Color.clear; //Clear should never be used by any laser
    }

    public bool HandleLaserHit(Color colorOfHittingLaser)
    {
        currentHittingColor = colorOfHittingLaser;
        return isSatisfied();
    }

    public bool isSatisfied()
    {
        if (currentHittingColor == inputColorNeeded)
            return true;

        return false;
    }
}
