using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InRoom))]
public class Skeletos : Enemy, IFacingMover
{
    [Header("Inscribed: Skeletos")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Dynamic: Skeletos")]
    [Range(0, 4)]
    public int facing = 0;
    public float timeNextDecision = 0;

    private InRoom inRm;

    protected override void Awake()
    {
        base.Awake();
        inRm = GetComponent<InRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timeNextDecision)
        {
            DecideDirection();
        }
        // rigid is inherited form Enemy, which defines it in Enemy.Awake()
        rigid.velocity = directions[facing] * speed;
    }

    void DecideDirection()
    {
        facing = Random.Range(0, 5);
        timeNextDecision = Time.time + Random.Range(timeThinkMin, timeThinkMax);
    }

    public int GetFacing() => facing % 4;
    public float GetSpeed() => speed;
    public bool moving { get => facing < 4; }
    public float gridMult { get => inRm.gridMult; }
    public bool isInRoom { get => inRm.isInRoom; }
    public Vector2 roomNum
    {
        get => inRm.roomNum;
        set => inRm.roomNum = value;
    }
    public Vector2 posInRoom
    {
        get => inRm.posInRoom;
        set => inRm.posInRoom = value;
    }
    public Vector2 GetGridPosInRoom(float mult = -1)
        => inRm.GetGridPositionInRoom(mult);
}
