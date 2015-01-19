using UnityEngine;
using System.Collections;

public class FireLaser : MonoBehaviour {

    public Direction direction;
    public bool Enabled;
    public Color OutLaserColor;
    
    Vector3 trueDirection;
    LineRenderer line;

    void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
        UpdateDirection();

        if (!Enabled)
            return;

        Fire();
    }

    public void Fire()
    {
        line.enabled = true;
        UpdateDirection();

        Ray ray = new Ray(transform.position + new Vector3(0, 0, -0.1f), trueDirection);


        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Get the distance to the center of the collider and save it as an offset
            float halfSize = ((hit.collider as BoxCollider).size.x/2f)*hit.transform.localScale.x;
            Vector3 offset = new Vector3(halfSize, halfSize, halfSize);

            offset.x = offset.x * trueDirection.x;
            offset.y = offset.y * trueDirection.y;
            offset.z = offset.z * trueDirection.z;

            Debug.Log("offset.magnitude " + offset.magnitude);

            //Draw the line
            line.SetPosition(0, ray.origin );
            line.SetPosition(1, ray.origin + trueDirection * hit.distance + offset);

            

            LaserHit laserHit = hit.transform.gameObject.GetComponent<LaserHit>();
            laserHit.HandleLaserHit(trueDirection, OutLaserColor);
        }
        else
        {
            line.SetPosition(0, ray.origin);
            line.SetPosition(1, ray.GetPoint(100));

            Debug.Log("I hit nothing!");
        }
    }

    void UpdateDirection()
    {
        switch (direction)
        {
            case Direction.Up:
                trueDirection = new Vector3(0, 1, 0);
                break;
            case Direction.Down:
                trueDirection = new Vector3(0, -1, 0);
                break;
            case Direction.Right:
                trueDirection = new Vector3(1, 0, 0);
                break;
            case Direction.Left:
                trueDirection = new Vector3(-1, 0, 0);
                break;
            default:
                break;
        }
    }

    public Direction GetDirectionFromVector3(Vector3 direction)
    {
        if (direction.y == 0)
        {
            if (direction.x == 1)
                return Direction.Right;
            else
                return Direction.Left;
        }
        else if (direction.y == 1)
            return Direction.Up;
        else
            return Direction.Down;
    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }
}
