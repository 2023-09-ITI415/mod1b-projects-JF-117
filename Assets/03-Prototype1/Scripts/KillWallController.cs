using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWallController : MonoBehaviour
{
    public float movementSpeed;
    public float acceleration;
    float timer = 0;
    bool timerReached = false;

    void Update() {

        if (!timerReached)
        timer += Time.deltaTime;
        
        if (!timerReached && timer > 5){;
        movementSpeed = movementSpeed + acceleration * Time.deltaTime;
        Vector3 pos = transform.position;
        if (pos.z < 300){
        pos.z += movementSpeed * Time.deltaTime;
        transform.position = pos;
        }
        else
        gameObject.SetActive (false);
        }
    }


}
