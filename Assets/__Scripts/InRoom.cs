using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRoom : MonoBehaviour
{
    static public float ROOM_W = 16;
    static public float ROOM_H = 11;
    static public float WALL = 2;

    // Where is this character in local room coordinates?
    public Vector2 posInRoom
    {
        get
        {
            Vector2 tPos = transform.position;
            Vector2 posIR = new Vector2();
            posIR.x = tPos.x % ROOM_W;
            posIR.y = tPos.y % ROOM_H;
            return posIR;
        }
        set
        {
            Vector2 rNum = roomNum;
            Vector2 tPos = new Vector2();
            tPos.x = roomNum.x * ROOM_W;
            tPos.y = roomNum.y * ROOM_H;
            transform.position = tPos + value;
        }
    }

    // Which room is this character in?
    public Vector2 roomNum
    {
        get
        {
            Vector2 tPos = (Vector2)transform.position;
            Vector2 rNum = new Vector2
            {
                x = Mathf.Floor(tPos.x / ROOM_W),
                y = Mathf.Floor(tPos.y / ROOM_H)
            };
            return rNum;
        }
        set
        {
            Vector2 rNum = value;
            Vector2 tPos = new Vector2
            {
                x = rNum.x * ROOM_W,
                y = rNum.y * ROOM_H
            };
            transform.position = tPos + posInRoom;
        }
    }
}
