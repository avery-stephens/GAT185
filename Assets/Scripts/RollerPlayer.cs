using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerPlayer : MonoBehaviour
{
    [SerializeField] private float maxForce = 5;
    private Vector3 force;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        force = direction * maxForce;

        if (Input.GetButtonDown("Jump")) 
        {
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

	private void FixedUpdate()
	{
        rb.AddForce(force);	
	}
}
