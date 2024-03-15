using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private int max_x;
    [SerializeField]
    private int max_y;
    [SerializeField]
    private int cellSize;

    public MapTile TilePrefab;
    MapTile[,] mapTiles;
    bool[,] check;

    void Start()
    {
        GenerateMap(40, 40, 2);

    }

    void GenerateMap(int _max_x, int _max_y, int _cellSize) 
    {
        max_x = _max_x;
        max_y = _max_y;
        cellSize = _cellSize;

        mapTiles = new MapTile[max_y, max_x];
        check = new bool[max_y, max_x];

        for (int i = 0; i < max_y; i++)
        {
            for (int j = 0; j < max_x; j++)
            {
                if (i % 2 != 0 &&  j % 2 != 0)
                    check[i, j] = true;
            }
        }

        int x = max_x / 2;
        int y = 0;

        mapGenerate(x, y);
    }

    void mapGenerate(int x, int y)
    {
        Debug.Log(x);
        Debug.Log(y);
        if (x < 0 || x >= max_x) return;

        check[y, x]= true;
        mapTiles[y, x] = Instantiate(TilePrefab, transform);
        mapTiles[y, x].init(x, y);
        mapTiles[y, x].transform.position -= new Vector3(cellSize * x, cellSize * y, 0);

        if (y == max_y - 1)
        {
            mapTiles[y, x].setSprite(MapTile.mapType.End);
            return;
        }

        var move = new List<int[]>();

        if (y < max_y - 1 && !check[y + 1, x])
        {
            move.Add(new int[] { 0, 1 });
        }
        if (x < max_x - 1 &&!check[y, x + 1])
        {
            move.Add(new int[] {1, 0});
        }
        if (x > 0 && !check[y, x - 1])
        {
            move.Add(new int[] { -1, 0 });
        }

        if (move.Count == 0)
        {
            Debug.Log("No Path");
            return;
        }

        Debug.Log(move);
        int way = UnityEngine.Random.Range(1, move.Count + 1);
        int choice = UnityEngine.Random.Range(0, move.Count);
        switch (way)
        {
            case 1:
                mapGenerate(x + move[choice][0], y + move[choice][1]);
                break;
            case 2:
                for (int i = 0; i < move.Count; i++)
                {
                    if (i == choice) continue;
                    mapGenerate(x + move[i][0], y + move[i][1]);
                }
                break;
            case 3:
                for (int i = 0; i < move.Count; i++)
                {
                    mapGenerate(x + move[i][0], y + move[i][1]);
                }
                break;
        }

        
    }
}
