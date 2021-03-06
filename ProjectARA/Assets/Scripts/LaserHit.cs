﻿using UnityEngine;
using System.Collections;

public class LaserHit : MonoBehaviour {

    public GameObject gameObjectThatHitMe = null;
    public Vector3 directionOfLaserHittingMe;
    public Color colorOfLaserHittingMe;

    TileType tileScript;
    FireLaser fireLaserScript;
    SpriteRenderer sprite;

    GameObject gameController;

    void Awake()
    {
        tileScript = gameObject.GetComponent<TileType>();
        fireLaserScript = gameObject.GetComponent<FireLaser>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void Update()
    {
        if (gameObjectThatHitMe == null)
        {
            if(tileScript.Type == TileType.TypeOfTile.Destructible)
                sprite.color = Color.cyan;
            else if (tileScript.Type == TileType.TypeOfTile.Wall)
                sprite.color = Color.black;
            else
                sprite.color = Color.grey;
        }
    }

    //Handle the hit from the laser based on what type of Tile we are
    //The direction and color is of the laser that hit us
    public void HandleLaserHit(Vector3 direction, Color color, GameObject hitter)
    {
        //Makes sure this object was not hit beforehand!
        //TODO: Handle things that can get hit by more than one thing
        if (gameObjectThatHitMe != null)
        {
            //If we're NOT being hit by what hit us earlier, return
            if(gameObjectThatHitMe != hitter)
                return;
        }

        //Store that we hit this gameobject in the gameobject that hit us
        hitter.GetComponent<FireLaser>().gameObjectHitByMyLaser = gameObject;
        
        color.a = 0.6f;

        //Store a reference to the gameobject that hit us
        gameObjectThatHitMe = hitter;
        directionOfLaserHittingMe = direction;
        colorOfLaserHittingMe = color;

        switch (tileScript.Type)
        {
            case TileType.TypeOfTile.Empty:
                break;

            case TileType.TypeOfTile.Emitter:
                break;

            case TileType.TypeOfTile.Receiver:
                Tile_Receiver receiverScript = gameObject.GetComponent<Tile_Receiver>();
                if (receiverScript.HandleLaserHit(color))
                    gameController.GetComponent<CheckWinConditions>().DidWeWin();
                else 
                    Debug.Log(color + " is not the right color");
                break;

            case TileType.TypeOfTile.Prism:
                sprite.color = color;
                Tile_Prism mirrorScript = gameObject.GetComponent<Tile_Prism>();
                FireLaser.Direction newDirection;
                if (mirrorScript.PrismHit(direction, out newDirection))
                {
                    fireLaserScript.OutLaserColor = color;
                    fireLaserScript.direction = newDirection;
                    fireLaserScript.Enabled = true;
                    fireLaserScript.Fire();
                }
                else
                {
                    fireLaserScript.Enabled = false;
                }
                break;

            case TileType.TypeOfTile.Straight:
                sprite.color = color;
                Tile_Straight straightScript = gameObject.GetComponent<Tile_Straight>();
                Color newColor;
                if (straightScript.AllowLaserToPassThrough(direction))
                {
                    straightScript.StraightHit(color, out newColor);
                    fireLaserScript.direction = fireLaserScript.GetDirectionFromVector3(direction);
                    fireLaserScript.OutLaserColor = newColor;
                    fireLaserScript.Enabled = true;
                    fireLaserScript.Fire();
                }
                else
                {
                    fireLaserScript.Enabled = false;
                }
                break;

            case TileType.TypeOfTile.Filter:
                
                Tile_Filter filterScript = gameObject.GetComponent<Tile_Filter>();
                
                if (filterScript.AllowLaserToPassThrough(direction))
                {
                    sprite.color = color + filterScript.FilterColor;
                    fireLaserScript.direction = fireLaserScript.GetDirectionFromVector3(direction);
                    fireLaserScript.OutLaserColor = filterScript.FilterColor;
                    fireLaserScript.Enabled = true;
                    fireLaserScript.Fire();
                }
                else
                {
                    fireLaserScript.Enabled = false;
                }
                break;

            case TileType.TypeOfTile.Wall:
                break;
                
            case TileType.TypeOfTile.Destructible:
                break;

            default:
                break;
        }
    }
}
