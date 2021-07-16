using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleMove : MonoBehaviour {
	SimpleMoveConfig simpleMoveConfig;
    
    // Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    public void SetStartPoint(Vector3 startPoint){
        transform.position = startPoint;
    }

    public void SetSimpleMoveConfig(SimpleMoveConfig config){
        simpleMoveConfig = config;
    }

    private void Move()
    {
        if(simpleMoveConfig){
            // Debug.Log(transform.position.x + " " + transform.position.y + " " + simpleMoveConfig.GetEndY());

            Vector3 targetPosition = new Vector3(transform.position.x, simpleMoveConfig.GetEndY(), 0f);

            transform.position = Vector2.MoveTowards(
                transform.position, targetPosition, simpleMoveConfig.GetSpeed()
            );

            // if (transform.position == targetPosition)
            // {
            //     Destroy(gameObject);
            // }
        }
    }
}
