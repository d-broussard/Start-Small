using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public Light myLight;
    public float originalRange;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject startTextObject;
    private Rigidbody rb;
    private int count; 
    private float movementX;
    private float movementY;
    public GameObject player;
    private Vector3 scaleChange, positionChange;
    public GameObject startText2Object;
    public GameObject rampObject;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 6;

        SetCountText();
        winTextObject.SetActive(false);
        startTextObject.SetActive(true);
        myLight = GetComponent<Light>();
        originalRange = myLight.range;
        rampObject.SetActive(false);   

        

    
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
            Invoke("level2", 3);                //transitions to next level
        }

        if(count <=1)
        {
            rampObject.SetActive(true);         //activates ramp at the end of level 2
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
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);      //increases the size of the player after collecting each orb
            myLight.range = originalRange * 14/10;              //increases the range of the player component after collecting each orb

        }

        if(other.gameObject.CompareTag("restart"))
        {
          SceneManager.LoadScene(0);            //kills and restarts the player if they fall
        }

        if(other.gameObject.CompareTag("restart2"))
        {
            SceneManager.LoadScene(1);          //kills and restarts the player if they fall
        }

        if(other.gameObject.CompareTag("RemoveStartText"))
        {
            startText2Object.SetActive(false);         //removes the start text of level 2 after progressing forward
        }
    }

    public void level2()                // used to transition between levels
    {
        SceneManager.LoadScene(1);
        winTextObject.SetActive(false);
       
    }


}