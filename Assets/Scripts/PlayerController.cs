using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 20f;
    public LayerMask groundLayer;

    [SerializeField] private float speed = 100f;

    private float horizontalInput;
    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        rigidbody.AddForce(Vector3.right * (speed * horizontalInput));
        IsOnTheGround();
        if (Input.GetKeyDown(KeyCode.Space) && IsOnTheGround())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsOnTheGround()
    {
        //Declaración y configuración del rayo
        float yOffset = 0.2f;
        Vector3 origin = transform.position;
        SphereCollider playerCollider = GetComponent<SphereCollider>();

        Physics.Raycast(origin, Vector3.down, out RaycastHit hit, playerCollider.radius + yOffset, groundLayer);
        
        //Dibujo del rayo (operador ternario). Funciona como un if else siempre que solo incluyamos un únicvo caso verdadero y falso, si son más NO!
        Color raycastColor = hit.collider != null ? Color.green : Color.magenta;

        /*
        if(hit.collider != null ?)
        {
            raycastColor = Color.green;
        }
        else
        {
            raycastColor = Color.magenta;
        }
         */

        Debug.DrawRay(origin, Vector3.down * (playerCollider.radius + yOffset), raycastColor, 0, false);
        return hit.collider != null;
    }
}
