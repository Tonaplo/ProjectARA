using UnityEngine;
using System.Collections;

public class Tile_Straight : MonoBehaviour {

    FireLaser fireLaserScript;

    bool OneLaserHasHit = false;
    Color laserOneColor;
    Color laserTwoColor;

    void Awake()
    {
        fireLaserScript = gameObject.GetComponent<FireLaser>();
    }

    //TODO: write a function and updates and fires the new lasers!

    //laserColor is the color of the laser that hit us, newColor is the new color after crossing with another laser
    public void StraightHit(Color laserColor, out Color newColor)
    {
        if (OneLaserHasHit)
        {
            newColor = laserColor + laserOneColor;
        }

        OneLaserHasHit = true;
        laserOneColor = laserColor;
        newColor = fireLaserScript.OutLaserColor;
    }
}
