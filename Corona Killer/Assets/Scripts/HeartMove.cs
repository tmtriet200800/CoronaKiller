using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMove : MonoBehaviour
{
    HeartConfig heartConfig;

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetStartPoint(Vector3 startPoint)
    {
        transform.position = startPoint;
    }

    public void SetHeartConfig(HeartConfig config)
    {
        heartConfig = config;
    }

    private void Move()
    {
        if (heartConfig)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, heartConfig.GetEndY(), 0f);

            transform.position = Vector2.MoveTowards(
                transform.position, targetPosition, heartConfig.GetSpeed()
            );
        }
    }
}
