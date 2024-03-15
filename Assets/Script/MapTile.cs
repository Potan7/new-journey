using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public int x, y;
    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer spriteRenderer;

    public enum mapType
    {
        Start,
        Enemy,
        Camp,
        Event,
        Elite,
        End
    }

    public void init(int x, int y)
    {
        this.x = x;
        this.y = y;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setSprite(mapType tile)
    {
        spriteRenderer.sprite = sprites[(int)tile];
    }
}
