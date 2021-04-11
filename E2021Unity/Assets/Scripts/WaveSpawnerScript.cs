using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WaveSpawnerScript : MonoBehaviour
{
	[System.Serializable]
    public class Wave
	{
		public string nameOfWave;
		public Transform enemyPreFab;
		public int count;
		public float rateSpawn;
		

	}
	//public string enemyTag;
	private float delay = 4f;
	public Wave[] waves;
	private int nextWave = 0;
	public float countdownSearch = 1f;
	public float timeBetween = 6f;
	public float waveCountDown;
	public Transform SpawnPoint1;
	public Transform SpawnPoint2;
	public Transform SpawnPoint3;
	private int loopCount;
	

	public enum SpawnState { SPAWNING, KILLING, COUNTING};
	public SpawnState state = SpawnState.KILLING;


	 void Start()
	{
		waveCountDown = timeBetween;
	}


	void Update()
	{
		if(state == SpawnState.KILLING)
		{
			if(CheckEnemyAlive() == false )
			{
				state = SpawnState.COUNTING;
				waveCountDown = timeBetween;
				//Debug.Log("Wave not started or wave complete");
			
			}
			else {
				//found enemies with the tagname enemy
				return; 
			}
		}




		// spawning wave if countdown is zero
		if (waveCountDown <= 0)
		{
			// testing
			if (state == SpawnState.KILLING || state == SpawnState.COUNTING)
			{
				//spawning wave with index of nextwave
				StartCoroutine(SpawnWave(waves[nextWave]));
			}
		}
		else
		//if not spawning the timer goes down
		{
			waveCountDown -= Time.deltaTime;
		}
	}

	IEnumerator SpawnWave(Wave wave_)
	{
		//spawning
		state = SpawnState.SPAWNING;

		for(int i = 0; i < wave_.count; i++)
		{
			loopCount = i;
			SpawnEnemy(wave_.enemyPreFab, loopCount);

			//makes a delay for each enemy
			
			yield return new WaitForSeconds(delay);
		}

		//done spawning
		state = SpawnState.KILLING;

		//return nothing
		yield break;
	}

	void SpawnEnemy(Transform enemy_, int loopcounter)
	{
		//Debug.Log("Trying  to spawn");
		//spawning one enemy

		//alternates beetween the 3 spawns with modulus
		if(loopcounter % 3 == 0)
		{
			Instantiate(enemy_, SpawnPoint1.position, transform.rotation);
			//adding collider, centering the collider and freezing rigidbody rotation
			CapsuleCollider cp = enemy_.gameObject.AddComponent<CapsuleCollider>();
			
			cp.height = 2;
			cp.center = new Vector3(0f, 1.5f, 0f);
			Rigidbody rb = enemy_.gameObject.GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
			
			
		}
		else if (loopcounter % 3 == 1)
		{
			Instantiate(enemy_, SpawnPoint2.position, transform.rotation);
			//adding collider, centering the collider and freezing rigidbody rotation
			CapsuleCollider cp = enemy_.gameObject.AddComponent<CapsuleCollider>();
			cp.height = 2;
			cp.center = new Vector3(0f, 1.3f, 0f);
			Rigidbody rb = enemy_.gameObject.GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		} 
		else if(loopcounter % 3 == 2)
		{
			Instantiate(enemy_, SpawnPoint3.position, transform.rotation);
			//adding collider, centering the collider and freezing rigidbody rotation
			CapsuleCollider cp = enemy_.gameObject.AddComponent<CapsuleCollider>();
			cp.height = 2;
			cp.center = new Vector3(0f, 1.1f, 0f);
			Rigidbody rb = enemy_.gameObject.GetComponent<Rigidbody>();
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		}


	}
	//method to check if enemies are alive with a timer to not check each frame
	bool CheckEnemyAlive()
	{
		
		countdownSearch -= Time.deltaTime;
		if(countdownSearch <= 0f)
		{
			countdownSearch = 1f;
			
			if (GameObject.FindGameObjectWithTag("enemy") == null)
			{
				//didn't found an enemy returning false
				return false;
			}
		}

		//found enemy with enemyTag enemy returning true
		
		return true; 
	}
}
