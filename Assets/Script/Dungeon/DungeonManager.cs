using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public int mapLenght = 20;
    private int _moveCnt;
    public int moveCnt
    {
        get
        {
            return _moveCnt;
        }
        set
        {
            _moveCnt = value;
            UIManager.ChangeDiceText(_moveCnt);
        }
    }

    public MapGenerator mapGen;
    public PlayerMovement player;
    public DUIManager UIManager;

    private void Start()
    {
        var playerPos = mapGen.GenerateMap(mapLenght);
        // 맵 생성 및 Start 칸을 플레이어 위치로 설정

        Camera.main.transform.position = playerPos.transform.position + new Vector3(0, 0, -10f);
        player.transform.position = playerPos.transform.position + new Vector3(0, 0, -1f);
        // 플레이어와 카메라의 위치를 Start 칸으로 이동
        player.pos = playerPos;
    }

    public void diceRoll(int cnt)
    {
        moveCnt = cnt;
        player.CheckMovement();
    }

    public void EndTurn()
    {
        Debug.Log(player.pos.tileType);
        player.pos.pass = true;
        UIManager.EnableRollButton();
    }
}
