using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public string stringToEdit = "Hello World\nI've got 2 lines...";
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag =="Player") {
						stringToEdit = GUI.TextArea (new Rect (10, 10, 200, 100), stringToEdit, 200);
			Debug.Log("Exit");
				}
	}
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
