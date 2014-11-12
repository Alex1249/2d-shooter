using UnityEngine;

public class AI : MonoBehaviour {
	[SerializeField]
	private Actor actor = null;
	
	[SerializeField]
	private float viewDistance = 10f, shootDistance = 6f, pursuitTime = 10f;
	
	private Transform self = null;
	private Transform player = null;
	
	private float viewTime = 0f;
	
	private int layerPlayerRay = 0, layerGroundRay = 0, layerPlayer = 0, layerPlayerBullet = 0;
	
	private void View() {
		if  (player == null) {
			return;
		}
		
		Vector3 pos = self.position;
		Vector3 dir = self.localScale.x > 0f ? Vector3.right : Vector3.left;
		
		viewTime = Mathf.Max(0f, viewTime - Time.smoothDeltaTime);
		
		RaycastHit hit;
		
		if  (Physics.Raycast(new Ray(pos, dir), out hit, viewDistance, layerPlayerRay | layerGroundRay)) {
			if  (hit.collider.gameObject.layer == layerPlayer) {
				viewTime = pursuitTime;
			}
		}
		
		if  (Vector3.Distance(player.position, pos) > viewDistance * 2f) {
			viewTime = 0f;
		}
	}
	
	private void Shoot() {
		if  (player == null) {
			return;
		}
		
		actor.Shoot();
	}
	
	private void PursuitRun(Vector3 selfPos, Vector3 playerPos, Vector3 dir) {
		if  (player == null) {
			viewTime = 0f;
			return;
		}


		
		if  (dir.x < 0f) {
			actor.Run(-1f, 180f, -1.1f);
			if  (actor.IsGroundLeft) {
				
				actor.Jump();
			}
		} else {
			actor.Run(1f, 0f, 0.6f);
			if  (actor.IsGroundRight) {
				
				actor.Jump();
			}
		}
	}
	
	private void Pursuit() {
		if  (player == null) {
			return;
		}
		
		if  (viewTime > 0f) {
			Vector3 selfPos = self.position;
			Vector3 playerPos = player.position;
			float dist = Vector3.Distance(playerPos, selfPos);
			Vector3 dir = playerPos.x > selfPos.x ? Vector3.right : Vector3.left;
			
			if  (dist <= shootDistance && Mathf.RoundToInt(playerPos.y - selfPos.y) == 0) {
				Vector3 curDir = self.localScale.x > 0f ? Vector3.right : Vector3.left;
				
				if  (curDir == dir) {
					Shoot();
				} else {
					PursuitRun(selfPos, playerPos, dir);
				}
			} else {
				PursuitRun(selfPos, playerPos, dir);
			}
		}
	}
	
	private float patrolTime = 0f;
	private float patrolType = 0f;
	
	private void Patrol() {
		patrolTime = Mathf.Max(0f, patrolTime - Time.smoothDeltaTime);
		
		if  (viewTime == 0f) {
			if  (patrolTime == 0f || (patrolType > 0f && actor.IsGroundRight) || (patrolType < 0f && actor.IsGroundLeft)) {
				patrolTime = Random.value + Random.value + 1f;
				patrolType = Mathf.Round((Random.value - 0.5f) * 2f);
			}
			
			if  (patrolType != 0f) {
				if  (Physics.Raycast(new Ray(self.position + new Vector3(patrolType * 1.75f, 0f, 0f), Vector3.down), 0.8f, layerGroundRay)) {
					actor.Run(patrolType, 0f, 0.6f);
				}
			}
		}
	}
	
	void Awake() {
		self = transform;
		
		player = GameObject.Find("player").transform;
		layerPlayerRay = 1 << LayerMask.NameToLayer("player");
		layerGroundRay = 1 << LayerMask.NameToLayer("ground");
		layerPlayer = LayerMask.NameToLayer("player");
		layerPlayerBullet = LayerMask.NameToLayer("bulletPlayer");
	}
	
	void Update() {
		View();
		Pursuit();
		Patrol();
	}
	
	void OnCollisionEnter(Collision collision) {
		int l = collision.collider.gameObject.layer;
		
		if  (l == layerPlayer || l == layerPlayerBullet) {
			viewTime = pursuitTime;
		}
	}
}