using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthNDrops : MonoBehaviour
{

    public int hitPoints; // total number of hitpoints before the enemy dies
    public GameObject[] expPoints; // An array of the experience points items that the enemy will drop on death

    public bool expSpawned = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hitPoints <= 0)
		{
            SpawnExp();
            //Play a destruction animation JUICED
            //Play a sound effect JUICED
            Destroy(gameObject, 0.5f);
		}
    }

    private void SpawnExp()
	{
        //int quality = Random.Range(0, 1);
        
        if (expSpawned == false)
		{
            Instantiate(expPoints[0], gameObject.transform.position, gameObject.transform.rotation);
            expSpawned = true;
        }

        
	}
}
