using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum direction
    {
        left, right, up, down, all
    }
    /// <summary>
    /// 0 - Left
    /// 1 - Right
    /// 2 - Up
    /// 3 - Down
    /// 4 - All
    /// </summary>
    public GameObject[] arrows;

    public DungeonManager dungeon;
    MapGenerator map; 
    public MapTile pos;

    int canMove = 0;
    bool playerClick = true;
    bool isMove = false;

    // 맵이 원점 대칭으로 뒤집어진 상태라 시점과 좌표가 서로 반대임.
    int[,] move = new int[,] { { 1, 0 }, { -1, 0 }, { 0, -1 }, { 0, 1 } };

    float moveSpeed = 0.1f;

    private void Start()
    {
        map = dungeon.mapGen;
    }

    public void CheckMovement()
    {
        canMove = 0;
        for (int i = 0; i < 4; i++)
        {
            if (map.isPlayerCanMove(pos, move[i, 0], move[i, 1])) {
                arrows[i].SetActive(true);
                canMove++;
            }
            else
            {
                arrows[i].SetActive(false);
            }
        }

        if (dungeon.moveCnt > 0)
        {
            arrows[(int)direction.all].SetActive(true);
        }
    }

    public bool CheckMovement(direction dir)
    {
        return map.isPlayerCanMove(pos, move[(int)dir, 0], move[(int)dir, 1]);
    }

    public void moveLeft()
    { 
        if (playerClick)
        {
            StartCoroutine(keepGoing(moveLeft, direction.left));
        }
        else
        {
            var toMove = map.getMapTile(pos.x + 1, pos.y);
            StartCoroutine(moveX(toMove.transform.position.x, -moveSpeed));
            pos = toMove;
        }
    }

    public void moveRight()
    {
        if (playerClick)
        {
            StartCoroutine(keepGoing(moveRight, direction.right));
        }
        else
        {
            var toMove = map.getMapTile(pos.x - 1, pos.y);
            StartCoroutine(moveX(toMove.transform.position.x, moveSpeed));
            pos = toMove;
        }
    }

    public void moveUp()
    {
        if (playerClick)
        {
            StartCoroutine(keepGoing(moveUp, direction.up));
        }
        else
        {
            var toMove = map.getMapTile(pos.x, pos.y - 1);
            StartCoroutine(moveY(toMove.transform.position.y, moveSpeed));
            pos = toMove;
        }
    }

    public void moveDown()
    {
        if (playerClick)
        {
            StartCoroutine(keepGoing(moveDown, direction.down));
        }
        else
        {
            var toMove = map.getMapTile(pos.x, pos.y + 1);
            StartCoroutine(moveY(toMove.transform.position.y, -moveSpeed));
            pos = toMove;
        }
    }

    void moveDone()
    {
        isMove = false;

        transform.position = pos.transform.position + new Vector3(0, 0, -1f);
        Camera.main.transform.position = pos.transform.position + new Vector3(0, 0, -10f);

        if (pos.tileType == MapTile.mapType.End) 
        {
            StopAllCoroutines();
            dungeon.moveCnt = 0;
            dungeon.EndTurn();
            Debug.Log("END");
            return;
        }

        dungeon.moveCnt -= 1;
        CheckMovement();

        if (dungeon.moveCnt == 0) dungeon.EndTurn();
    }

    IEnumerator moveX(float target, float speed, float time = 0.01f)
    {
        isMove = true;
        arrows[(int)direction.all].SetActive(false);
        if (speed > 0)
        {
            while (transform.position.x < target)
            {
                transform.position += new Vector3(speed, 0);
                Camera.main.transform.position = transform.position + new Vector3(0, 0, -10f);
                yield return new WaitForSeconds(time);
            }
        }
        else
        {
            while (transform.position.x > target)
            {
                transform.position += new Vector3(speed, 0);
                Camera.main.transform.position = transform.position + new Vector3(0, 0, -10f);
                yield return new WaitForSeconds(time);
            }
        }
        moveDone();
    }

    IEnumerator moveY(float target, float speed, float time = 0.01f)
    {
        isMove = true;
        arrows[4].SetActive(false);
        if (speed > 0)
        {
            while (transform.position.y < target)
            {
                transform.position += new Vector3(0, speed);
                Camera.main.transform.position = transform.position + new Vector3(0, 0, -10f);
                yield return new WaitForSeconds(time);
            }
        }
        else
        {
            while (transform.position.y > target)
            {
                transform.position += new Vector3(0, speed);
                Camera.main.transform.position = transform.position + new Vector3(0, 0, -10f);
                yield return new WaitForSeconds(time);
            }
        }
        moveDone();
    }

    IEnumerator keepGoing(Action moveFunc, direction dir)
    {
        direction reverse = direction.all;
        switch (dir)
        {
            case direction.left:
                reverse = direction.right;
                break;
            case direction.right:
                reverse = direction.left;
                break;
            case direction.up:
                reverse = direction.down;
                break;
            case direction.down:
                reverse = direction.up;
                break;

        }

        playerClick = false;
        while (dungeon.moveCnt > 0)
        {
            moveFunc.Invoke();
            yield return new WaitForSeconds(0.1f);
            yield return new WaitWhile(() => isMove);

            if (canMove > 2)
            {
                playerClick = true;
                arrows[(int)reverse].SetActive(false);
                break;
            }
            else if (!CheckMovement(dir))
            {
                direction newDir = direction.all;
                for (int i = 0; i < 4; i++)
                {
                    if (map.isPlayerCanMove(pos, move[i, 0], move[i, 1]) && i != (int)reverse)
                    {
                        newDir = (direction)i;
                        break;
                    }
                }

                if (newDir == direction.all) //막다른길 대비용
                {
                    newDir = reverse;
                }

                switch (newDir)
                {
                    case direction.left:
                        StartCoroutine(keepGoing(moveLeft, newDir));
                        break;
                    case direction.right:
                        StartCoroutine(keepGoing(moveRight, newDir));
                        break;
                    case direction.up:
                        StartCoroutine(keepGoing(moveUp, newDir));
                        break;
                    case direction.down:
                        StartCoroutine(keepGoing(moveDown, newDir));
                        break;
                }

                break;
            }
        }
        playerClick = true;
    }

}
