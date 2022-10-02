using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float movementSpeed;
    public Transform target;
    public Rigidbody2D erb;

    public int damage = 1;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        erb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        erb.velocity = Vector2.zero;
        transform.position = Vector2.MoveTowards(gameObject.transform.position, target.position, movementSpeed * Time.deltaTime);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Player"))
		{
            collision.gameObject.GetComponent<PlayerController>().onTakeDamage(damage);
		}
	}

}
