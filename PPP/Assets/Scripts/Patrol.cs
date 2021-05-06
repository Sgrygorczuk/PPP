using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    //================ Movement 
    public Transform[] patrolPoints; //Holds all the postions that the enemy will travel
    public float speed; //Speed of the enemy 
    private int currentPointIndex; //Which point are they at right now
    
    //================== Pause 
    private float waitTime;   //Timer 
    public float startWaitTime; //What timer resets to 

    // Start is called before the first frame update
    void Start()
    {
        currentPointIndex = 1;
        transform.position = patrolPoints[0].position;
        transform.rotation = patrolPoints[0].rotation;
        waitTime = startWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        //Tells it to move from currently standing in point, to given point at the given speed 
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        transform.rotation = patrolPoints[currentPointIndex].rotation;

        if(transform.position == patrolPoints[currentPointIndex].position){
           
            if(waitTime <= 0){
                if(currentPointIndex == patrolPoints.Length - 1){
                  currentPointIndex = 0;
                }
                else{
                    currentPointIndex++;
                }
            }
            else{
                waitTime -= Time.deltaTime; 
            }

        }
    }
}
