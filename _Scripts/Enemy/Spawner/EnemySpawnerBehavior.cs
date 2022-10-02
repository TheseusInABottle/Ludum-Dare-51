using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBehavior : MonoBehaviour
{
    public GameObject enemy;

    public float detectionRadius;
    public LayerMask playerLayer;

    public Transform[] SpawnPoints;

    public float SPAWNENEMYTIME;
    public float spawn;

    public AudioSource spawnNoises;

    // Start is called before the first frame update
    void Start()
    {
        spawn = SPAWNENEMYTIME;
        spawnNoises = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.OverlapCircle(gameObject.transform.position, detectionRadius, playerLayer))
		{
            if(spawn <= 0)
			{
                SpawnEnemies();
                spawn = SPAWNENEMYTIME;
                spawnNoises.Play();
			}
			else
			{
                spawn -= Time.deltaTime;
			}
		}
    }

    private void SpawnEnemies()
	{
        int r = Random.Range(0, 4);

        Instantiate(enemy, SpawnPoints[r].position, gameObject.transform.rotation);
	}
}
