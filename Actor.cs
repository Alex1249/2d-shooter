using UnityEngine;

public class Actor : MonoBehaviour {
	[SerializeField]
	private Rigidbody2D body = null;
	[SerializeField]
	private Transform  bulletSpawn = null, bulletPrefab = null;
	public Transform  weapon = null;
	private Transform self = null;
	
	[SerializeField]
	private float maxSpeed = 50f, force = 10f, jumpForce = 16f, jumpWait = 0.2f;
	
	private int layerGround = 0; 
	
	private float reload = 0f, jumpReload = 0f;
	public float dir = -1f;
	public float  time = 0f;
	[SerializeField]
	private int lifes = 3;

	void Awake() {
		self= transform;
		layerGround = 1 << LayerMask.NameToLayer("ground");
	}

	void LateUpdate() {
		if (self.position.y < -55f){
			Destroy (gameObject);
		}
		UpdateWeapon();
	}
	
	private void UpdateWeapon() {
		reload = Mathf.Max(0f, reload - Time.smoothDeltaTime);
		jumpReload = Mathf.Max(0f, jumpReload - Time.smoothDeltaTime);
	}

	public void Jump() {
		if  (jumpReload == 0f) {
			Vector2 vel = body.velocity;
			vel.y = jumpForce;
			body.velocity = vel;
			jumpReload = jumpWait;
			}
	}

	public void Run(float direction, float rotation, float posZ) {
		Vector2 vel = body.velocity;
		dir = direction;
		vel.x += force * direction;

		if  (Mathf.Abs(vel.x) > maxSpeed) {
			vel.x = maxSpeed * direction;	
		}
		
		body.velocity = vel;
		weapon.localRotation =  Quaternion.Euler(0f, rotation, 0f);
		weapon.localPosition = new Vector3 (0f ,0f, posZ);
		
	}
	
	public void Shoot() {
		if (reload == 0f && dir != 0f) {
			(Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.Euler(0f, 0f, 0f)) as Transform).GetComponent<bullet>().dir = new Vector3(dir * 20f, (Random.value - 0.5f) * 0.5f, 0f);
			reload = 0.1f;
		}
	}

	public bool IsGroundDown {
		get {
			return Physics2D.Raycast(self.position, new Vector2(0,-1), 1f , layerGround );
		}
	}
	
	public bool IsGroundLeft {
		get {
			return Physics2D.Raycast(self.position, new Vector2(-1,0), 1f, layerGround);
		}
	}
	
	public bool IsGroundRight {
		get {
			return Physics2D.Raycast(self.position, new Vector2(1,0), 1f, layerGround);
		}
	}
	public bool IsGroundUp {
		get {
			return Physics2D.Raycast(self.position, new Vector2(0,1), 1f, layerGround );
			
		}
	}
	
	public void Hit(Vector3 point, Vector3 direction) {
		lifes = Mathf.Max(0, lifes - 1);
	}
}