using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    public enum mapType
    {
        Start,
        Enemy,
        Camp,
        Event,
        Elite,
        End
    }

    public int x, y;
    public int size;
    [SerializeField]
    Sprite[] sprites;

    SpriteRenderer spriteRenderer;
    public mapType tileType;
    public bool pass = false;
    

    public void init(Transform parent, int x, int y, mapType type)
    {
        this.x = x;
        this.y = y;
        transform.position = parent.position - new Vector3(size * x, size * y, 0);

        spriteRenderer = GetComponent<SpriteRenderer>();
        setType(type);
    }

    public void setType(mapType type)
    {
        spriteRenderer.sprite = sprites[(int)type];
        tileType = type;
    }
}
