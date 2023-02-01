using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Coin : Interactable
{
	[SerializeField] AudioSource interactSound;
	void Start()
	{
		GetComponent<CollisionEvent>().onEnter += OnInteract;
	}

	public override void OnInteract(GameObject go)
	{
		if (go.TryGetComponent<RollerPlayer>(out RollerPlayer player))
		{
			player.AddPoints(100);
			interactSound.Play();
		}

		if (interactFX != null) Instantiate(interactFX, transform.position, Quaternion.identity);
		if (destroyOnInteract) Destroy(gameObject);
	}
}