using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaMovement : MonoBehaviour
{
    [Header("Banana Position")]
    [SerializeField] private float leftBorderBanana;
    [SerializeField] private float rightBorderBanana;

    [Header("Banana Movement")]
    [SerializeField] private float horizontalSpeedBanana = 5f;
    [SerializeField] private float maxForceBanana = 50f;

    private Vector3 mousePos;
    private Rigidbody rb;
    private bool isSliding;
    private float mousePressedTime;
    private float mouseReleasedTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            transform.gameObject.tag = "BananaMove";
            
            if (rb.velocity.z == 0)
            {
                transform.gameObject.tag = "Banana";
                isSliding = false;
            }
        }
        else
        {
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
            mousePos.x = Mathf.Clamp(mousePos.x, leftBorderBanana, rightBorderBanana);
            transform.position = Vector3.Lerp(transform.position, new Vector3(mousePos.x, transform.position.y, transform.position.z), Time.deltaTime * horizontalSpeedBanana);
        }

        if (Input.GetMouseButtonDown(0) && !isSliding)
        {

            mousePressedTime = Time.time;
        }

        if (Input.GetMouseButtonUp(0) && !isSliding)
        {
            mouseReleasedTime = Time.time;
            float shootForce = Mathf.Clamp((mouseReleasedTime - mousePressedTime) * maxForceBanana, 0, maxForceBanana);
            rb.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
            isSliding = true;
        }

        if (transform.position.y < -1f)
        {
            Destroy(gameObject);
        }
    }
}
