using UnityEngine;
using System.Collections;

public class DragonCharacterController : MonoBehaviour {

	// Public atributes
	public float verticalSpeed;
	public float horizontalSpeed;

	public float verticalRotationSpeed;
	public float horizontalRotationSpeed;

	public float recoverRotationSpeed;

	public float verticalMaxDistance;
	public float horizontalMaxDistance;

	public float LookAtPointDistance;

	public GameObject characterDirector;

	public GameObject projectile;

	public Texture crosshairImage;

	// Private atributes
	private float xDistance;
	private float yDistance;

	private float xLookAtDistance;
	private float yLookAtDistance;

	private Camera mainCamera;

	// Use this for initialization
	void Start () {
		if (characterDirector)
		{
			mainCamera = GameObject.FindGameObjectWithTag("MainCamera").camera;

			transform.position = characterDirector.transform.position;
			transform.rotation = characterDirector.transform.rotation;

			xDistance = 0.0f;
			yDistance = 0.0f;

			xLookAtDistance = 0.0f;
			yLookAtDistance = 0.0f;

			Vector3 LookAtPoint = characterDirector.transform.position + (characterDirector.transform.forward.normalized * LookAtPointDistance);
			transform.LookAt(LookAtPoint);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (characterDirector)
		{
			// ----------------- Get horizontal and vertical axis
			float yTranslation = Input.GetAxis("Vertical");
			float xTranslation = Input.GetAxis("Horizontal");
			yTranslation *= Time.deltaTime;
			xTranslation *= Time.deltaTime;
			// -----------------

			// ----------------- Calc character position
			xDistance += xTranslation * horizontalSpeed;
			yDistance += yTranslation * verticalSpeed;

			xDistance = Mathf.Clamp(xDistance, -horizontalMaxDistance, horizontalMaxDistance);
			yDistance = Mathf.Clamp(yDistance, -verticalMaxDistance, verticalMaxDistance);

			Vector3 characterPosition = characterDirector.transform.position;
			characterPosition = characterPosition + characterDirector.transform.right.normalized * xDistance;
			characterPosition = characterPosition + characterDirector.transform.up.normalized * yDistance;

			transform.position = characterPosition;
			// -----------------

			// ----------------- Calc character look at point
			xLookAtDistance += xTranslation * horizontalRotationSpeed;
			yLookAtDistance += yTranslation * verticalRotationSpeed;

			xLookAtDistance = Mathf.Lerp(xLookAtDistance, 0, Time.deltaTime * recoverRotationSpeed);
			yLookAtDistance = Mathf.Lerp(yLookAtDistance, 0, Time.deltaTime * recoverRotationSpeed);

			Vector3 LookAtPoint = characterDirector.transform.position + (characterDirector.transform.forward.normalized * LookAtPointDistance);
			LookAtPoint = LookAtPoint + characterDirector.transform.right.normalized * xLookAtDistance;
			LookAtPoint = LookAtPoint + characterDirector.transform.up.normalized * yLookAtDistance;

			transform.LookAt(LookAtPoint);
			// -----------------

			if (Input.GetButtonDown("Fire1") && projectile)
			{
				Instantiate(projectile, transform.position + transform.forward.normalized * 3, transform.rotation);
			}
		}
	}

	void OnGUI () {
		Vector3 LookAtPoint = characterDirector.transform.position + (characterDirector.transform.forward.normalized * LookAtPointDistance);
		LookAtPoint.x += xLookAtDistance;
		LookAtPoint.y += yLookAtDistance;
		Vector3 ScreenPoint = mainCamera.WorldToScreenPoint(LookAtPoint);

		Rect crosshairPosition = new Rect(ScreenPoint.x - 25, (mainCamera.pixelHeight - ScreenPoint.y) - 25, 50, 50);
		GUI.DrawTexture(crosshairPosition, crosshairImage);
	}
}
