using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Public atributes
	public Transform target;

	public float targetDistance;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (target)
		{
			transform.position = target.position - (target.forward.normalized * targetDistance);
		}
	}
}
