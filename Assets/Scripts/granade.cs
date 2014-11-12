using UnityEngine;

public class granade : MonoBehaviour {
	[SerializeField]
	private float timer = 5f;
	private float time = 0f;
	
	[SerializeField]
	private Transform fxPrefab = null;

	void Awake() {
		time = timer;
	}
	
	void Update () {
		time -= Time.smoothDeltaTime;
		
		if  (time <= 0f) {
			Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f,  1 << LayerMask.NameToLayer("ground") | 1 << LayerMask.NameToLayer("player") | 1 << LayerMask.NameToLayer("playerTwo") | 1 << LayerMask.NameToLayer("enemy"));
			
			foreach (Collider hit in hits) {
				Actor actor = hit.GetComponent<Actor>();
				
				if  (actor != null) {
					actor.Explosion();
				} else {
					ground g = hit.GetComponent<ground>();
					
					if  (g != null) {
						g.Kill(0.5f);
					}
				}
			}
			
			((Transform)Instantiate(fxPrefab, transform.position, Quaternion.identity)).GetComponent<ParticleEmitter>().Emit();
			
			Destroy(gameObject);
		}
	}
}