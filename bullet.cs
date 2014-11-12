using UnityEngine;

public class bullet : MonoBehaviour {
	public Vector3 dir = Vector3.zero;
	
	private Transform self = null;
	
	void Awake() {
		self = transform;
	}
	
	void Update() {
		self.position += dir * Time.smoothDeltaTime;
	}
	
	void OnCollisionEnter(Collision collision) {
		Destroy(gameObject);
	}
	
	void OnCollisionStay(Collision collision) {
		Destroy(gameObject);
	}
	
	void OnCollisionExit(Collision collision) {
		Destroy(gameObject);
	}
}