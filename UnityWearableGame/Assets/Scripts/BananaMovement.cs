using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMovement : MonoBehaviour
{
    [Header("Banana Position")]
    [SerializeField] private float leftBorder;
    [SerializeField] private float rightBorder;

    [Header("Banana Movement")]
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private float maxForce = 50f;

    private Vector3 mousePos;
    private Rigidbody rb;
    private bool isSliding;
    private float mousePressedTime;
    private float mouseReleasedTime;

    private GameObject playerGO;
    private Player playerNow;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerGO = GameObject.Find("Players");
        playerNow = playerGO.GetComponent<PlayerChange>().currentPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            if (GetComponent<Rigidbody>().velocity.magnitude < 0.01f && playerNow.points > 0)
            {
                playerNow.isSelectingItem = true;
            }
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            mousePos.x = Mathf.Clamp(mousePos.x, leftBorder, rightBorder);
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeed);
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
            Destroy(gameObject);
        }
    }
}
