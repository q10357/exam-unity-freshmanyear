using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampage : MonoBehaviour
{
	// Start is called before the first frame update


	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Colliding with" + other);
		//Checks if the object colliding with powerup is the player
		if (other.CompareTag("Player"))
		{
			Debug.Log("colliding with player");
			PickUp(other);
		}
	}


	public void PickUp(Collider other)
	{
		Debug.Log("Rampage");

		Destroy(gameObject);
	}
}
