using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public float DECAYTIME = 3f;
    public float destroyTime;

    public AudioClip hitSound;

    //public float spinSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        destroyTime = DECAYTIME;
    }

    // Update is called once per frame
    void Update()
    {
        hitSound = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().userClass.weaponSound;
        gameObject.transform.Rotate(Vector3.forward);

        if(destroyTime <= 0)
		{
            Destroy(gameObject, 0.01f);
		}
		else
		{
            destroyTime -= Time.deltaTime;
		}
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Enemy"))
		{
            AudioSource sfx = GameObject.FindGameObjectWithTag("SFXer").GetComponent<AudioSource>();
            sfx.clip = hitSound;
            sfx.Play();
            int damage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().userClass.damage;
            collision.gameObject.GetComponent<EnemyHealthNDrops>().hitPoints -= damage;
            Destroy(gameObject, 0.01f);
		}
		else
		{
            AudioSource sfx = GameObject.FindGameObjectWithTag("SFXer").GetComponent<AudioSource>();
            sfx.clip = hitSound;
            Destroy(gameObject, 0.01f);
		}
	}
}
