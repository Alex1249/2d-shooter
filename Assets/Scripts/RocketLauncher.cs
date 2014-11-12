using UnityEngine;

public class RocketLauncher : MonoBehaviour {
	
	[SerializeField]
	private Transform fxPrefab = null;
	
	public Vector3 dir = Vector3.zero;
	
	private Transform self = null;
	

void Awake() {
		self = transform;
	}
	
	void Update() {
		self.position += dir * Time.smoothDeltaTime;
	}
	
	void OnCollisionEnter(Collision collision) {
	
			Collider[] hits = Physics.OverlapSphere(transform.position, 1.5f, 1 << LayerMask.NameToLayer("ground") | 1 << LayerMask.NameToLayer("player") | 1 << LayerMask.NameToLayer("playerTwo") | 1 << LayerMask.NameToLayer("enemy"));
			
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
