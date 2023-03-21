using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletos : Enemy
{
    [Header("Inscribed: Skeletos")]
    public int speed = 2;
    public float timeThinkMin = 1f;
    public float timeThinkMax = 4f;

    [Header("Dynamic: Skeletos")]
    [Range(0, 4)]
    public int facing = 0;
    public float timeNextDecision = 0;

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
}
