using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    int mapLen;

    private int max_x;
    private int max_y;

    int start_x;
    int start_y;

    public MapTile TilePrefab;
    MapTile[,] mapTiles;
    bool[,] check;

    public MapTile GenerateMap(int maxLen) 
    {
        mapLen = maxLen;
        max_x = mapLen * 2;
        max_y = mapLen * 2;

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

        start_x = max_x / 2;
        start_y = 0;

        mapGenerate(start_x, start_y, 0);

        mapTiles[start_y, start_x].setType(MapTile.mapType.Start);
        return mapTiles[start_y, start_x];
    }

    void mapGenerate(int x, int y, int time)
    {
        if (x < 0 || x >= max_x) return;

        check[y, x]= true;
        mapTiles[y, x] = Instantiate(TilePrefab, transform);
        if (time % 6 == 0)
        {
            mapTiles[y, x].init(transform, x, y, MapTile.mapType.Camp);
        }
        else if (time > max_y)
        {
            int random = UnityEngine.Random.Range(0, 4);
            if (random == 0) mapTiles[y, x].init(transform,x, y, MapTile.mapType.Event);
            else if (random == 1) mapTiles[y, x].init(transform, x, y, MapTile.mapType.Elite);
            else mapTiles[y, x].init(transform, x, y, MapTile.mapType.Enemy);
        }
        else
        {
            int random = UnityEngine.Random.Range(0, 3);
            if (random == 0) mapTiles[y, x].init(transform, x, y, MapTile.mapType.Event);
            else mapTiles[y, x].init(transform, x, y, MapTile.mapType.Enemy);
        }
        
        //mapTiles[y, x].transform.position -= new Vector3(cellSize * x, cellSize * y, 0);

        if (time >= mapLen)
        {
            mapTiles[y, x].setType(MapTile.mapType.End);
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
            //Debug.Log("No Path");
            return;
        }

        int way = UnityEngine.Random.Range(1, move.Count + 1);
        int choice = UnityEngine.Random.Range(0, move.Count);
        switch (way)
        {
            case 1:
                mapGenerate(x + move[choice][0], y + move[choice][1], time + 1);
                break;
            case 2:
                for (int i = 0; i < move.Count; i++)
                {
                    if (i == choice) continue;
                    mapGenerate(x + move[i][0], y + move[i][1], time + 1);
                }
                break;
            case 3:
                for (int i = 0; i < move.Count; i++)
                {
                    mapGenerate(x + move[i][0], y + move[i][1], time + 1);
                }
                break;
        }

        
    }

    public bool isPlayerCanMove(MapTile playerPos, int moveX, int moveY)
    {
        int pX = playerPos.x;
        int py = playerPos.y;

        if (pX + moveX >= max_x || py + moveY >= max_y) return false;
        if (pX + moveX < 0 || py + moveY < 0) return false;

        if (mapTiles[py+moveY, pX+moveX] != null)
        {
            return true;
        }
        return false;
    }

    public MapTile getMapTile(int x, int y)
    {
        if (x < 0 || y < 0) return null;
        if (x >= max_x || y >= max_y) return null;

        return mapTiles[y, x];
    }
}
