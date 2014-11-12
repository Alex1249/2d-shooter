using UnityEngine;

public class granade : MonoBehaviour {
	[SerializeField]
	private float timer = 5f;
	private float time = 0f;

	void Awake() {
		time = timer;
	}
	
	void Update () {
		time -= Time.smoothDeltaTime;
		
		if  (time <= 0f) {
			Destroy(gameObject);
		}
	}
}