using UnityEngine;
using System.Collections;

public class EmitterRayCaster : MonoBehaviour {

    public Direction direction;

    Vector2 trueDirection;
    LayerMask triangleTileLayer;

    void Awake()
    {
        triangleTileLayer = LayerMask.GetMask("TriangleTile");
        switch (direction)
        {
            case Direction.Up:
                trueDirection = new Vector2(0, 1);
                break;
            case Direction.Down:
                trueDirection = new Vector2(0, -1);
                break;
            case Direction.Right:
                trueDirection = new Vector2(1, 0);
                break;
            case Direction.Left:
                trueDirection = new Vector2(-1, 0);
                break;
            default:
                break;
        }

        UpdateLaser();
    }

    void UpdateLaser()
    {
        Vector2 startPos = transform.position;
        Vector2 direction = trueDirection;
        bool blocked = false;
        int hitLayer = 0;

        while (!blocked)
        {
            RaycastHit2D hit = Physics2D.Raycast(startPos, direction, 10);
            Debug.Log("Distance: " + hit.distance + " HitLayer: " + hitLayer);
            Debug.DrawLine(startPos, startPos + direction * hit.distance, Color.black, 4.0f);
            if (hitLayer == triangleTileLayer)
            {
                //hit.transform.get
                //Write logic about bouncing and stuff
            }
            else
            {
                blocked = true;
            }
        }
    }

    public enum Direction
    {
        Up, 
        Down,
        Right,
        Left
    }
}
