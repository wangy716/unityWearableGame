using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    [Header("Banana Position")]
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    [Header("Banana Movement")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float maxForce = 50f;

    private Vector3 mousePos;
    private Rigidbody rb;
    private Collider collider;
    private bool isSliding;
    private float mousePressedTime;
    private float mouseReleasedTime;

    private GameObject playerGO;
    private Player playerNow;

    private PlayerChange playerChange;
    private ArduinoConnect arduinoConnect;
    private float handPosMap;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        playerGO = GameObject.Find("Players");
        playerNow = playerGO.GetComponent<PlayerChange>().currentPlayer;
        playerChange = playerGO.GetComponent<PlayerChange>();

        arduinoConnect = GameObject.Find("Arduino").GetComponent<ArduinoConnect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            if (playerNow.isShooting && GetComponent<Rigidbody>().velocity.magnitude < 0.01f)
            {
                if (gameObject.CompareTag("BananaMove"))
                {
                    gameObject.tag = "Banana";


                }
                else if (gameObject.CompareTag("GumMove"))
                {
                    gameObject.tag = "Gum";

                }
                else if (gameObject.CompareTag("TrapMove"))
                {
                    gameObject.tag = "Trap";

                }

                if (playerNow.points > 0)
                {
                    playerNow.isSelectingItem = true;
                    playerNow.isShooting = false;
                    
                } else
                {
                    playerChange.EndRound();
                }
                this.enabled = false;
            }
        }
        else
        {
            //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            //mousePos.x = Mathf.Clamp(mousePos.x, leftBorder, rightBorder);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeed);

            handPosMap = arduinoConnect.handPos;
            //Debug.Log(handPosMap);
            handPosMap = Mathf.Lerp(leftBorder, rightBorder, (handPosMap - 10f) / 20f);
            transform.position = Vector3.Lerp(transform.position, new Vector3(handPosMap, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeed);
        }

        if (Input.GetMouseButtonDown(0) && !isSliding && playerNow.isShooting)
        {

            mousePressedTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0) && !isSliding && playerNow.isShooting)
        {
            mouseReleasedTime = Time.time;
            float shootForce = Mathf.Clamp((mouseReleasedTime - mousePressedTime) * maxForce, 0, maxForce);
            rb.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
            isSliding = true;
        }

        if (transform.position.y < -1f)
        {
            if (playerNow.points <= 0)
            {
                playerChange.EndRound();
            }
            playerNow.isSelectingItem = true;
            playerNow.isShooting = false;
            Destroy(gameObject);
            
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("sushi"))
        {
            playerNow.isShooting = false;
        }
    }
}
