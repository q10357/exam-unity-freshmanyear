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
	public Transform SpawnPoint;
	//public Transform SpawnPoint1;
	//public Transform SpawnPoint2;

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
				Debug.Log("Wave not started or wave complete");
			
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
			SpawnEnemy(wave_.enemyPreFab);

			//makes a delay for each enemy
			
			yield return new WaitForSeconds(delay);
		}

		//done spawning
		state = SpawnState.KILLING;

		//return nothing
		yield break;
	}

	void SpawnEnemy(Transform enemy_)
	{
		Debug.Log("Trying  to spawn");
		//spawning one enemy
		Instantiate(enemy_, SpawnPoint.position, transform.rotation);
		

	}
	//method to check if enemies are alive with a timer to not check each frame
	bool CheckEnemyAlive()
	{
		Debug.Log("checking enemy alive");
		countdownSearch -= Time.deltaTime;
		if(countdownSearch <= 0f)
		{
			countdownSearch = 1f;
			Debug.Log("countdownsearch udner 0 ");
			if (GameObject.FindGameObjectWithTag("enemy") == null)
			{
				Debug.Log("Found no enemy bitch");
				return false;
			}
		}

		//found enemy with enemyTag
		Debug.Log("Found enemy bitch");
		return true; 
	}
}
