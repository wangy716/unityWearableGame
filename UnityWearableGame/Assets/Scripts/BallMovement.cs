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

    private GameObject playerGO;
    private Player playerNow;

    private Vector3 mousePos;
    protected Rigidbody rb;
    private bool isSliding;
    private float mousePressedTime;
    private float mouseReleasedTime;

    private ArduinoConnect arduinoConnect;
    private float middlePoint = 30f;
    private float handPosMap;

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerGO = GameObject.Find("Players");
        playerNow = playerGO.GetComponent<PlayerChange>().currentPlayer;

        arduinoConnect = GameObject.Find("Arduino").GetComponent<ArduinoConnect>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSliding)
        {

        } else
        {
            //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //mousePos.x = Mathf.Clamp(mousePos.x, leftBorder, rightBorder);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeed);

            handPosMap = arduinoConnect.handPos;
            //Debug.Log(handPosMap);
            handPosMap = Mathf.Lerp(leftBorder, rightBorder, (handPosMap - 10f) / 20f);
            transform.position = Vector3.Lerp(transform.position, new Vector3(handPosMap, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeed);

        }
        
        if(Input.GetMouseButtonDown(0) && !isSliding && playerNow.isShooting)
        {
            
            mousePressedTime = Time.time;   
        }

        if(Input.GetMouseButtonUp(0) && !isSliding && playerNow.isShooting)
        {
            mouseReleasedTime = Time.time;
            float shootForce = Mathf.Clamp((mouseReleasedTime - mousePressedTime) * maxForce, 0, maxForce);
            rb.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
            isSliding = true;
        }

        if(transform.position.y < -1f)
        {
            playerNow.isShooting = false;
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            rb.AddForce(new Vector3(Random.Range(-1.5f, 1.5f), 0, 1) * bananaForce, ForceMode.Impulse);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Gum"))
        {
            rb.velocity *= gumForce;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Trap"))
        {
            playerNow.isShooting = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

}
    