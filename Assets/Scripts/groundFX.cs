using UnityEngine;

public class groundFX : MonoBehaviour {

	private Vector3 vel;
	private float time;
	
	void Awake() {
		vel = new Vector3((Random.value - 0.5f) * 3f, (Random.value) * 5f + 3f, 0f);
		time = Random.value * 0.3f + 0.3f;
	}
	
	void Update() {
		vel.y -= Time.smoothDeltaTime * 30f;
		transform.position += (vel * Time.smoothDeltaTime);
		
		time -= Time.smoothDeltaTime;
		
		if  (time <= 0f) {
			Destroy(gameObject);
		}
	}
	
	void OnDestroy() {
		if  (Application.isPlaying) {
			Destroy(GetComponent<MeshFilter>().sharedMesh);
		}
	}
}