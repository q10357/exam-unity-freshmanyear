using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampageBuffScript : MonoBehaviour
{
	// Start is called before the first frame update

	public float damagebuff = 3f;
	public float durationbuff = 5f;
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Colliding with" + other);
		//Checks if the object colliding with powerup is the player
		if (other.CompareTag("Player"))
		{
			StartCoroutine(PickUpRampage(other));
		}
	}


	public IEnumerator PickUpRampage(Collider other)
	{
		Debug.Log("Rampage");

		Gunshooting gunstats = other.GetComponentInChildren<Gunshooting>();
		
		//adding the damage buff
		gunstats.damage *= damagebuff;
		
		//wait for the duration

		yield return new WaitForSeconds(durationbuff);

		//reverting to noraml stats

		gunstats.damage /= damagebuff;

		Destroy(gameObject);
	}
}
