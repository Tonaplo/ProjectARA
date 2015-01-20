using UnityEngine;
using System.Collections;

public class Tile_Mirror : MonoBehaviour {

    public MirrorReflectionDirection ReflectionDirection;

    FireLaser fireLaserScript;
    LaserHit laserHitScript;

    void Awake()
    {
        fireLaserScript = gameObject.GetComponent<FireLaser>();
        laserHitScript = gameObject.GetComponent<LaserHit>();

        switch (ReflectionDirection)
        {
            case MirrorReflectionDirection.UpLeft:
                transform.Rotate(Vector3.forward * -90f);
                break;
            case MirrorReflectionDirection.LeftDown:
                break;
            case MirrorReflectionDirection.DownRight:
                transform.Rotate(Vector3.forward * 90f);
                break;
            case MirrorReflectionDirection.RightUp:
                transform.Rotate(Vector3.forward * 180f);
                break;
            default:
                break;
        }
    }

    //Direction is the direction of the recieved laser
    public bool MirrorHit(Vector3 direction, out FireLaser.Direction newDirection)
    {
        if (direction.y == 0)
        {
            if (direction.x == 1)
            {
                if (ReflectionDirection == MirrorReflectionDirection.LeftDown)
                {
                    newDirection = FireLaser.Direction.Down;
                    return true;
                }
                else if (ReflectionDirection == MirrorReflectionDirection.UpLeft)
                {
                    newDirection = FireLaser.Direction.Up;
                    return true;
                }
            }
            else
            {
                if (ReflectionDirection == MirrorReflectionDirection.DownRight)
                {
                    newDirection = FireLaser.Direction.Down;
                    return true;
                }
                else if (ReflectionDirection == MirrorReflectionDirection.RightUp)
                {
                    newDirection = FireLaser.Direction.Up;
                    return true;
                }
            }
        }
        else if (direction.y == 1)
        {
            if (ReflectionDirection == MirrorReflectionDirection.DownRight)
            {
                newDirection = FireLaser.Direction.Right;
                return true;
            }
            else if (ReflectionDirection == MirrorReflectionDirection.LeftDown)
            {
                newDirection = FireLaser.Direction.Left;
                return true;
            }
        }
        else
        {
            if (ReflectionDirection == MirrorReflectionDirection.RightUp)
            {
                newDirection = FireLaser.Direction.Right;
                return true;
            }
            else if (ReflectionDirection == MirrorReflectionDirection.UpLeft)
            {
                newDirection = FireLaser.Direction.Left;
                return true;
            }

        }

        newDirection = FireLaser.Direction.Up;
        return false;
    }

    public enum MirrorReflectionDirection
    {
        UpLeft,
        LeftDown,
        DownRight,
        RightUp
    }

    void OnMouseDown()
    {
        

        transform.Rotate(Vector3.forward * -90f);

        if (ReflectionDirection == MirrorReflectionDirection.DownRight)
            ReflectionDirection = MirrorReflectionDirection.LeftDown;
        else if (ReflectionDirection == MirrorReflectionDirection.LeftDown)
            ReflectionDirection = MirrorReflectionDirection.UpLeft;
        else if (ReflectionDirection == MirrorReflectionDirection.UpLeft)
            ReflectionDirection = MirrorReflectionDirection.RightUp;
        else
            ReflectionDirection = MirrorReflectionDirection.DownRight;

        //Tell the object we were hitting, that we're not hitting it anymore
        if (fireLaserScript.gameObjectHitByMyLaser != null)
        {
            FireLaser fireLaserOfTargetScript = fireLaserScript.gameObjectHitByMyLaser.GetComponent<FireLaser>();
            fireLaserOfTargetScript.StopFiring();

            fireLaserScript.gameObjectHitByMyLaser = null;
        }

        if (laserHitScript.gameObjectThatHitMe != null)
        {
            //Stop firing and handle refiring if we're actually supposed to
            fireLaserScript.Enabled = false;
            FireLaser fireLaserOfHitterScript = laserHitScript.gameObjectThatHitMe.GetComponent<FireLaser>();
            fireLaserOfHitterScript.Fire();
        }

        
    }
}
