using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text countText;
    public Text winText;
    public float jumpHeight;

    private Rigidbody rb;
    private int count;

    float timer = 0;
    bool timerReached = true;
    float starttimer = 0;
    bool starttimerReached = false;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void Update ()
    {
        //game over conditions
        if(!timerReached)
        timer += Time.deltaTime;

        if (rb.transform.position.y < -10f){
        timerReached = false;
        winText.text = "Game Over! Restarting...";
        //timer += Time.deltaTime;
        }

        if (!timerReached && timer > 3){
        SceneManager.LoadScene("Main-Prototype 1");
        //timerReached = true;
        }

        if (!starttimerReached)
        starttimer += Time.deltaTime;

        if (!starttimerReached && starttimer < 5){
        winText.text = "Don't Touch The Red! Press Space to Jump!";
        }
        else if (!starttimerReached && starttimer > 5){
        starttimerReached = true;
        winText.text = "";
        }
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
        //jumping
        if (Input.GetKeyDown ("space") && rb.transform.position.y <= 1f) {
            Vector3 jump = new Vector3 (0.0f, jumpHeight, 0.0f);
 
        rb.AddForce (jump);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
        }

        if(other.gameObject.CompareTag("KillWall"))
        {
            timerReached = false;
            winText.text = "Game Over! Restarting...";
        }
    
        if(other.gameObject.CompareTag("Goal"))
        {
             if (count >= 8)
        {
            winText.text = "You Win!";
        }
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString ();
    }
}
