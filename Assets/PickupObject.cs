using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {
	GameObject mainCamera;
	bool carrying;
	bool rotating;
	bool coloring;
	public GameObject carriedObject;
	public float distance;
	public float smooth;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
		if (carrying) {
			carry (carriedObject);
			checkDrop ();
		} 
//		else if (rotating) {
//			rotateObject (carriedObject);
//			checkDrop ();
//		}
		if(coloring){
			makeColor (carriedObject);
			checkStop (carriedObject);
		}
		else {
			pickup();
			changeColor ();
			//checkRotate ();
		}
	}
	void checkStop(GameObject c){
		if(Input.GetKeyDown (KeyCode.C)) {
			c.GetComponent<ObjectColor>().c.GetComponent<ColorPicker>().useExternalDrawer = true;
			coloring = false;
			carriedObject = null;
		}
	}
	void makeColor(GameObject c){
		c.GetComponent<ObjectColor>().c.GetComponent<ColorPicker>().useExternalDrawer = false;
	
	}
	void changeColor(){
		if(Input.GetKeyDown (KeyCode.C)) {
			Debug.Log ("coloring");
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				ObjectColor c = hit.collider.GetComponent<ObjectColor>();
				if(c != null) {
					coloring = true;
					carriedObject = c.gameObject;
					//p.gameObject.rigidbody.isKinematic = true;
					//c.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
		}
	}

	void checkRotate(){
		if(Input.GetKeyDown (KeyCode.R)) {
			Debug.Log ("rotating");
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null) {
					rotating = true;
					carriedObject = p.gameObject;
					//p.gameObject.rigidbody.isKinematic = true;
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
		}
	}
	void rotateObject(GameObject r) {
		Debug.Log ("rotate around");
		if (Input.GetMouseButton (0)) {
			r.transform.Rotate (0,(Input.GetAxis("Mouse X")* 5 * -Time.deltaTime),0,Space.World);
		}


		//		Quaternion newRotation = Quaternion.AngleAxis(90, Vector3.up);
		//		r.transform.rotation= Quaternion.Slerp(r.transform.rotation, newRotation, .05f); 
		//r.transform.Rotate(0,r.transform.rotation.y+90,0);
		//carriedObject.transform.Rotate(5,10,15);
	}

	void carry(GameObject o) {
		o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
		o.transform.rotation = Quaternion.identity;
	}

	void pickup() {
		
		if(Input.GetKeyDown (KeyCode.E)) {
			Debug.Log ("picking up");
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x,y));
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Pickupable p = hit.collider.GetComponent<Pickupable>();
				if(p != null) {
					carrying = true;
					carriedObject = p.gameObject;
					//p.gameObject.rigidbody.isKinematic = true;
					p.gameObject.GetComponent<Rigidbody>().useGravity = false;
				}
			}
		}
	}

	void checkDrop() {
		if(Input.GetKeyDown (KeyCode.E)) {
			dropObject();
		}
	}

	void dropObject() {
		rotating = false;
		carrying = false;
		//carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = false;
		carriedObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
		Debug.Log ("Dropdown");
		//carriedObject.gameObject.GetComponent<Rigidbody>().isKinematic = true;
		carriedObject = null;
	}
}
