using UnityEngine;
using System.Collections;

public class Tile_Mirror : MonoBehaviour {

    public MirrorReflectionDirection ReflectionDirection;

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
}
