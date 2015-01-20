using UnityEngine;
using System.Collections;

public class Tile_Prism : MonoBehaviour {

    public PrismReflectionDirection ReflectionDirection;

    FireLaser fireLaserScript;
    LaserHit laserHitScript;

    void Awake()
    {
        fireLaserScript = gameObject.GetComponent<FireLaser>();
        laserHitScript = gameObject.GetComponent<LaserHit>();

        switch (ReflectionDirection)
        {
            case PrismReflectionDirection.UpLeft:
                transform.Rotate(Vector3.forward * -90f);
                break;
            case PrismReflectionDirection.LeftDown:
                break;
            case PrismReflectionDirection.DownRight:
                transform.Rotate(Vector3.forward * 90f);
                break;
            case PrismReflectionDirection.RightUp:
                transform.Rotate(Vector3.forward * 180f);
                break;
            default:
                break;
        }
    }

    //Direction is the direction of the recieved laser
    public bool PrismHit(Vector3 direction, out FireLaser.Direction newDirection)
    {
        if (direction.y == 0)
        {
            if (direction.x == 1)
            {
                if (ReflectionDirection == PrismReflectionDirection.LeftDown)
                {
                    newDirection = FireLaser.Direction.Down;
                    return true;
                }
                else if (ReflectionDirection == PrismReflectionDirection.UpLeft)
                {
                    newDirection = FireLaser.Direction.Up;
                    return true;
                }
            }
            else
            {
                if (ReflectionDirection == PrismReflectionDirection.DownRight)
                {
                    newDirection = FireLaser.Direction.Down;
                    return true;
                }
                else if (ReflectionDirection == PrismReflectionDirection.RightUp)
                {
                    newDirection = FireLaser.Direction.Up;
                    return true;
                }
            }
        }
        else if (direction.y == 1)
        {
            if (ReflectionDirection == PrismReflectionDirection.DownRight)
            {
                newDirection = FireLaser.Direction.Right;
                return true;
            }
            else if (ReflectionDirection == PrismReflectionDirection.LeftDown)
            {
                newDirection = FireLaser.Direction.Left;
                return true;
            }
        }
        else
        {
            if (ReflectionDirection == PrismReflectionDirection.RightUp)
            {
                newDirection = FireLaser.Direction.Right;
                return true;
            }
            else if (ReflectionDirection == PrismReflectionDirection.UpLeft)
            {
                newDirection = FireLaser.Direction.Left;
                return true;
            }

        }

        newDirection = FireLaser.Direction.Up;
        return false;
    }

    public enum PrismReflectionDirection
    {
        UpLeft,
        LeftDown,
        DownRight,
        RightUp
    }

    void OnMouseDown()
    {
        transform.Rotate(Vector3.forward * -90f);

        if (ReflectionDirection == PrismReflectionDirection.DownRight)
            ReflectionDirection = PrismReflectionDirection.LeftDown;
        else if (ReflectionDirection == PrismReflectionDirection.LeftDown)
            ReflectionDirection = PrismReflectionDirection.UpLeft;
        else if (ReflectionDirection == PrismReflectionDirection.UpLeft)
            ReflectionDirection = PrismReflectionDirection.RightUp;
        else
            ReflectionDirection = PrismReflectionDirection.DownRight;

        //Tell the object we were hitting, that we're not hitting it anymore
        if (fireLaserScript.gameObjectHitByMyLaser != null)
        {
            FireLaser fireLaserOfTargetScript = fireLaserScript.gameObjectHitByMyLaser.GetComponent<FireLaser>();
            fireLaserOfTargetScript.StopFiring(0);

            LaserHit laserHitOfTargetScript = fireLaserScript.gameObjectHitByMyLaser.GetComponent<LaserHit>();
            laserHitOfTargetScript.gameObjectThatHitMe = null;

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
