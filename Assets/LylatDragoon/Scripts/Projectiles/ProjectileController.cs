using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	// Public atributes
	public float projectileSpeed;

	// Use this for initialization
	void Start () {
		rigidbody.AddForce(transform.forward.normalized * projectileSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col)
	{
		DestroyObject(this);
	}
}
