using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject startTextObject;
    private Rigidbody rb;
    private int count; 
    private float movementX;
    private float movementY;

    
    
    public GameObject player;
    private Vector3 scaleChange, positionChange;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 6;

        SetCountText();
        winTextObject.SetActive(false);
        startTextObject.SetActive(true);

    
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Light Orbs Left: " + count.ToString();
        
        if(count >=5)
        {
            startTextObject.SetActive(false);
        
        }

        if(count <= 0)
        {
            winTextObject.SetActive(true);
            
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup")) 
        {
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText ();
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
           

        }

        if(other.gameObject.CompareTag("restart"))
        {
          SceneManager.LoadScene(0);
        }
    }
}