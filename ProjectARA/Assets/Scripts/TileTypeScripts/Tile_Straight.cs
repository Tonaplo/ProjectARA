using UnityEngine;
using System.Collections;

public class Tile_Straight : MonoBehaviour {

    public StraightOrientation Orientation;

    FireLaser fireLaserScript;
    LaserHit laserHitScript;

    bool OneLaserHasHit = false;
    Color laserOneColor;
    Color laserTwoColor;

    void Awake()
    {
        fireLaserScript = gameObject.GetComponent<FireLaser>();
        laserHitScript = gameObject.GetComponent<LaserHit>();
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

    public bool AllowLaserToPassThrough(Vector3 laserDirection)
    {
        if (laserDirection.x != 0)
        {
            if (Orientation == StraightOrientation.Horizontal)
                return true;
            else
                return false;
        }
        else
        {
            if (Orientation == StraightOrientation.Vertical)
                return true;
            else
                return false;
        }
    }

    void OnMouseDown()
    {
        transform.Rotate(Vector3.forward * -90f);

        if (Orientation == StraightOrientation.Vertical)
            Orientation = StraightOrientation.Horizontal;
        else
            Orientation = StraightOrientation.Vertical;

        //Tell the object we were hitting, that we're not hitting it anymore
        if (fireLaserScript.gameObjectHitByMyLaser != null)
        {
            FireLaser fireLaserOfTargetScript = fireLaserScript.gameObjectHitByMyLaser.GetComponent<FireLaser>();
            fireLaserOfTargetScript.StopFiring();

            fireLaserScript.gameObjectHitByMyLaser = null;
        }

        laserHitScript.HandleLaserHit(laserHitScript.directionOfLaserHittingMe, laserHitScript.colorOfLaserHittingMe, laserHitScript.gameObjectThatHitMe);
        //fireLaserScript.Enabled = false;

        /*
        if (laserHitScript.gameObjectThatHitMe != null)
        {
            //Stop firing and handle refiring if we're actually supposed to
            fireLaserScript.Enabled = false;

            //Save a reference to the gameobject that's supposed to refire
            GameObject refiringGameObject = laserHitScript.gameObjectThatHitMe;

            //Reset our reference to that gameobject
            laserHitScript.gameObjectThatHitMe = null;

            //Fire from the previously stored reference
            refiringGameObject.GetComponent<FireLaser>().Fire();
        }
         * */


    }

    public enum StraightOrientation
    {
        Vertical,
        Horizontal
    }
}
