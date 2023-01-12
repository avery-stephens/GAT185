using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1,75), Tooltip("controls speed of the player")] public float speed = 5f;
    [Range(1,360)] public float rotationRate;
    public GameObject prefab;
    public Transform bulletSpawnLocation;

	private void Awake()
	{
        Debug.Log("awake");
	}

	// Start is called before the first frame update
	void Start()
    {
        Debug.Log("start");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(2, 3, 2);
        //transform.rotation = Quaternion.Euler(30, 45, 15);
        //transform.localScale = Vector3.one * Random.value * 5;

        Vector3 direction = Vector3.zero;
        direction.z = Input.GetAxis("Vertical");

        Vector3 rotation = Vector3.zero;
        rotation.y = Input.GetAxis("Horizontal");

        Quaternion rotate = Quaternion.Euler(rotation * rotationRate * Time.deltaTime);
        transform.rotation = transform.rotation * rotate;

        transform.Translate(direction * speed * Time.deltaTime);
        //transform.position += direction * speed * Time.deltaTime;

        if (Input.GetButton("Fire1"))
        {
            //Debug.Log("PEW PEW");
            //GetComponent<AudioSource>().Play();
            GameObject go = Instantiate(prefab, bulletSpawnLocation.position, bulletSpawnLocation.rotation);
        }
    }
}
