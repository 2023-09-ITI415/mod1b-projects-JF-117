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

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void Update ()
    {
        //out of bounds
        if (rb.transform.position.y < -10f){
        timerReached = false;
        winText.text = "Game Over! Restarting...";
        timer += Time.deltaTime;
        }

        if (!timerReached && timer > 3){
        SceneManager.LoadScene("Main-Prototype 1");
        timerReached = true;
        }

        
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
        //jumping
        if (Input.GetKeyDown ("space") && rb.transform.position.y <= 0.5f) {
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
    
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString ();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
