﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMove : MonoBehaviour {

    PathMoveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIndex = 0;

	// Use this for initialization
	void Start () {
        waypoints = waveConfig.GetWaypoints();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    public void SetPathMoveConfig(PathMoveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    
    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            
            transform.position = Vector2.MoveTowards
                (transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
