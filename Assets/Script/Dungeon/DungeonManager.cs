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
        // �� ���� �� Start ĭ�� �÷��̾� ��ġ�� ����

        Camera.main.transform.position = playerPos.transform.position + new Vector3(0, 0, -10f);
        player.transform.position = playerPos.transform.position + new Vector3(0, 0, -1f);
        // �÷��̾�� ī�޶��� ��ġ�� Start ĭ���� �̵�
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
