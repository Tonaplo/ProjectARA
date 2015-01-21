using UnityEngine;
using System.Collections;

public class Tile_Filter : MonoBehaviour {

    public Tile_Straight.StraightOrientation Orientation;
    public Color FilterColor;

    FireLaser fireLaserScript;
    LaserHit laserHitScript;

    void Awake()
    {
        fireLaserScript = gameObject.GetComponent<FireLaser>();
        laserHitScript = gameObject.GetComponent<LaserHit>();
        FilterColor.a = 0.6f;
    }

    public bool AllowLaserToPassThrough(Vector3 laserDirection)
    {
        if (laserDirection.x != 0)
        {
            if (Orientation == Tile_Straight.StraightOrientation.Horizontal)
                return true;
            else
                return false;
        }
        else
        {
            if (Orientation == Tile_Straight.StraightOrientation.Vertical)
                return true;
            else
                return false;
        }
    }

    void OnMouseDown()
    {
        transform.Rotate(Vector3.forward * -90f);

        if (Orientation == Tile_Straight.StraightOrientation.Vertical)
            Orientation = Tile_Straight.StraightOrientation.Horizontal;
        else
            Orientation = Tile_Straight.StraightOrientation.Vertical;

        //Tell the object we were hitting, that we're not hitting it anymore
        if (fireLaserScript.gameObjectHitByMyLaser != null)
        {
            FireLaser fireLaserOfTargetScript = fireLaserScript.gameObjectHitByMyLaser.GetComponent<FireLaser>();
            fireLaserOfTargetScript.StopFiring(0);

            fireLaserScript.gameObjectHitByMyLaser = null;
        }

        if (laserHitScript.gameObjectThatHitMe != null)
        {
            //Stop firing and handle refiring if we're actually supposed to
            fireLaserScript.Enabled = false;

            //Save a reference to the gameobject that's supposed to refire
            GameObject refiringGameObject = laserHitScript.gameObjectThatHitMe;

            //Reset our reference to that gameobject
            laserHitScript.gameObjectThatHitMe = null;

            //Simulate the hit from the previously firing gameObject
            laserHitScript.HandleLaserHit(laserHitScript.directionOfLaserHittingMe, laserHitScript.colorOfLaserHittingMe, refiringGameObject);
        }


    }
}
