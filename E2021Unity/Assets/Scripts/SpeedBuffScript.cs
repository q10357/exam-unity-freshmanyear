using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpeedBuffScript : MonoBehaviour
{
	public float speedbuff = 1.5f;
	public float time = 5f;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//sending player collision to pickup function
			StartCoroutine(PickUpSpeed(other));
		}
	}

	private IEnumerator PickUpSpeed(Collider other)
	{
		Debug.Log("Speed engaged");
		FirstPersonController stats = other.GetComponent<FirstPersonController>();
		stats.m_RunSpeed *= speedbuff;


		//wait some seconds before reverting the buff
		yield return new WaitForSeconds(time);

		//back to normal speed
		stats.m_RunSpeed /= speedbuff;


		Destroy(gameObject);
	}
}


