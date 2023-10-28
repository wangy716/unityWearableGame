using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Header("Ball Position")]
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    [Header("Ball Movement")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float maxForce = 50f;

    [Header("Banana")]
    [SerializeField] private float bananaForce = 5f;

    [Header("Gum")]
    [SerializeField] private float gumForce = 0.7f;

    private Vector3 mousePos;
    private Rigidbody rb;
    private bool isSliding;
    private float mousePressedTime;
    private float mouseReleasedTime;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSliding)
        {

        } else
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            mousePos.x = Mathf.Clamp(mousePos.x, leftBorder, rightBorder);
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeed);
        }
        
        if(Input.GetMouseButtonDown(0) && !isSliding)
        {
            
            mousePressedTime = Time.time;   
        }

        if(Input.GetMouseButtonUp(0) && !isSliding)
        {
            mouseReleasedTime = Time.time;
            float shootForce = Mathf.Clamp((mouseReleasedTime - mousePressedTime) * maxForce, 0, maxForce);
            rb.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
            isSliding = true;
        }

        if(transform.position.y < -1f)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Banana"))
        {
            rb.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 0, 1) * bananaForce, ForceMode.Impulse);
        }

        if (other.CompareTag("BananaMove"))
        {
            float bananaJump = 5f;
            rb.AddForce(Vector3.up * bananaJump);
        }

        if (other.CompareTag("Gum"))
        {
            rb.velocity *= gumForce;
        }

        if (other.CompareTag("Trap"))
        {
            Destroy(gameObject);
        }
    }
}
