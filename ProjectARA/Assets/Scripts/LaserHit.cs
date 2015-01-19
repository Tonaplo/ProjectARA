using UnityEngine;
using System.Collections;

public class LaserHit : MonoBehaviour {

    TileType tileScript;
    FireLaser fireLaserScript;

    void Awake()
    {
        tileScript = gameObject.GetComponent<TileType>();
        fireLaserScript = gameObject.GetComponent<FireLaser>();
        
    }

    //Handle the hit from the laser based on what type of Tile we are
    //The direction and color is of the laser that hit us
    public void HandleLaserHit(Vector3 direction, Color color)
    {
        switch (tileScript.Type)
        {
            case TileType.TypeOfTile.Empty:
                break;

            case TileType.TypeOfTile.Laser:
                break;

            case TileType.TypeOfTile.Mirror:
                Tile_Mirror mirrorScript = gameObject.GetComponent<Tile_Mirror>();
                FireLaser.Direction newDirection;
                if (mirrorScript.MirrorHit(direction, out newDirection))
                {
                    fireLaserScript.direction = newDirection;
                    fireLaserScript.Enabled = true;
                    fireLaserScript.Fire();
                }
                break;

            case TileType.TypeOfTile.Straight:
                Tile_Straight straightScript = gameObject.GetComponent<Tile_Straight>();
                Color newColor;

                straightScript.StraightHit(color, out newColor);
                fireLaserScript.direction = fireLaserScript.GetDirectionFromVector3(direction);
                fireLaserScript.OutLaserColor = newColor;
                fireLaserScript.Enabled = true;
                fireLaserScript.Fire();
                break;

            default:
                break;
        }
    }
}
