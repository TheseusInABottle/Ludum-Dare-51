using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);

    public Vector2[] limits;
    
    public float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void Update()
	{
        //gameObject.transform.position.x = Mathf.Clamp(gameObject.transform.position.x, limits[0].x, limits[1].x);
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, limits[0].x, limits[1].x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limits[0].y, limits[1].y);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
