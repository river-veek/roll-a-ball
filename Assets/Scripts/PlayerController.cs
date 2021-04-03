using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public Vector3 jump;
    //public int jumpPower = 2;
    //public Vector3 jump;

    //private bool jumping = false;
    private int count;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int numJumps;
    private bool jumped;
    // private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        numJumps = 0;

        winTextObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnJump()
    {
        jumped = true;
        numJumps++;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Ground")
        {
            numJumps = 0;
            jumped = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (numJumps <= 2 && jumped == true)
        {
            Vector3 jump = new Vector3(movementX, 2.0f, movementY);
            rb.AddForce(jump * 2.0f, ForceMode.Impulse);
            jumped = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
    }
}
