using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dray : MonoBehaviour, IFacingMover
{
    public enum eMode { idle, move, attack }

    [Header("Inscribed")]
    public float speed = 5;
    public float attackDuration = 0.25f; // Number of seconds to attack
    public float attackDelay = 0.5f; // Delay between attacks

    [Header("Dynamic")]
    public int dirHeld = -1; // Direction of the held movement key
    public int facing = 1; // Direction Dray is facing
    public eMode mode = eMode.idle;

    private float timeAtkDone = 0;
    private float timeAtkNext = 0;

    private Rigidbody2D rigid;
    private Animator anim;
    private InRoom inRm;

    private Vector2[] directions = new Vector2[]
    {
        Vector2.right, Vector2.up, Vector2.left, Vector2.down
    };

    private KeyCode[] keys = new KeyCode[]
    {
        KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow,
        KeyCode.D, KeyCode.W, KeyCode.A, KeyCode.S
    };

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inRm = GetComponent<InRoom>();
    }

    // Update is called once per frame
    void Update()
    {
        // Finishing the attack when it's over
        if (mode == eMode.attack && Time.time >= timeAtkDone)
        {
            mode = eMode.idle;
        }

        // Handle Keyboard Input in idle or move Modes
        if (mode == eMode.idle || mode == eMode.move)
        {
            dirHeld = -1;
            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKey(keys[i])) dirHeld = i % 4;
            }

            // Choosing the proper movement or idle mode based on dirHeld
            if (dirHeld == -1)
            {
                mode = eMode.idle;
            }
            else
            {
                facing = dirHeld;
                mode = eMode.move;
            }

            // Pressing the attack button
            if (Input.GetKeyDown(KeyCode.Z) && Time.time >= timeAtkNext)
            {
                mode = eMode.attack;
                timeAtkDone = Time.time + attackDuration;
                timeAtkNext = Time.time + attackDelay;
            }
        }

        // Act on the current mode
        Vector2 vel = Vector2.zero;
        switch (mode)
        {
            case eMode.attack: // Show the Attack pose in the correct direction
                anim.Play($"Dray_Attack_{facing}");
                anim.speed = 0;
                break;

            case eMode.idle: // Show frame 1 in the correct direction
                anim.Play($"Dray_Walk_{facing}");
                anim.speed = 0;
                break;

            case eMode.move:
                vel = directions[dirHeld];
                anim.Play($"Dray_Walk_{facing}");
                anim.speed = 1;
                break;
        }

        rigid.velocity = vel * speed;
    }

    public int GetFacing() => facing;
    public float GetSpeed() => speed;
    public bool moving { get => mode == eMode.move; }
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

    public Vector2 GetGridPosInRoom(float mult = -1) =>
        inRm.GetGridPositionInRoom(mult);
}
