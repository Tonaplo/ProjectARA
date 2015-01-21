using UnityEngine;
using System.Collections;

public class Tile_Destructible : MonoBehaviour
{
    public float timeForDestruction = 3f;
    float lerpTime = 0f;

    LaserHit laserHitScript;
    bool shotRegistered;
    SpriteRenderer spriteRenderer;

    Color lerpStartColor;
    Color lerpEndColor;

    void Awake()
    {
        laserHitScript = gameObject.GetComponent<LaserHit>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (laserHitScript.gameObjectThatHitMe != null)
        {
            if (!shotRegistered)
            {
                lerpStartColor = spriteRenderer.color;
                shotRegistered = true;
                lerpEndColor = laserHitScript.colorOfLaserHittingMe;
            }

            lerpTime += Time.deltaTime * timeForDestruction;
            spriteRenderer.color = Color.Lerp(lerpStartColor, lerpEndColor, lerpTime);

            if (spriteRenderer.color == lerpEndColor)
            {
                gameObject.GetComponent<BoxCollider>().enabled = false;
                laserHitScript.gameObjectThatHitMe.GetComponent<FireLaser>().Fire();
                Destroy(gameObject);
            }

        }
        else if (shotRegistered)
        {
            shotRegistered = false;
            lerpTime = 0f;
        }
    }
}
