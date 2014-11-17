using UnityEngine;
using System.Collections;

public class CharacterDirectorController : MonoBehaviour {

	// Public atributes
	public float forwardSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed, Space.Self);
	}
}
