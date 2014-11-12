using UnityEngine;

public class Actor : MonoBehaviour {
	[SerializeField]
	private Rigidbody body = null;
	[SerializeField]
	private Transform  bulletSpawn = null, bulletPrefab = null, granadePrefab = null,RocketPrefab = null, bloodPrefab = null;
	public Transform legs = null, weapon = null;
	private Transform self = null;
	
	[SerializeField]
	private float maxSpeed = 5f, force = 1f, jumpForce = 10f, granadeWait = 10f,rocketWait = 1f, jumpWait = 0.2f;
	
	private int layerGround = 0; //layerEnemy = 0, layerLadder = 0, layerPlayer = 0;
	
	private float reload = 0f, granadeReload = 0f, jumpReload = 0f, rocketReload = 0f;
	public float dir = -1f;
	public float  time = 0f;
	public bool ladder = false, ladderStop;
	[SerializeField]
	private int lifes = 3;
	
	void Awake() {
		self= transform;
		layerGround = 1 << LayerMask.NameToLayer("ground");
		//layerEnemy = 1 << LayerMask.NameToLayer("enemy");
		//layerLadder =  LayerMask.NameToLayer("ladder");
	}

	void LateUpdate() {
		if (self.position.y < -55f){
			Destroy (gameObject);
		}
		UpdateWeapon();
		RotateLegs();
	}
	
	private void RotateLegs() {
		float xVel = body.velocity.x;
		
		if  (Mathf.Abs(xVel) < 0.03f) {
			legs.rotation = Quaternion.RotateTowards(legs.rotation, Quaternion.identity, maxSpeed * Time.smoothDeltaTime * 10f);
		} else {
			legs.Rotate(0f, 0f, xVel * Time.smoothDeltaTime * -100f);
		}
	}
	
	private void UpdateWeapon() {
		reload = Mathf.Max(0f, reload - Time.smoothDeltaTime);
		granadeReload = Mathf.Max(0f, granadeReload - Time.smoothDeltaTime);
		rocketReload = Mathf.Max(0f, rocketReload - Time.smoothDeltaTime);
		jumpReload = Mathf.Max(0f, jumpReload - Time.smoothDeltaTime);
	}

	public void Jump() {
		if  (jumpReload == 0f) {
			Vector3 vel = body.velocity;
			vel.y = jumpForce;
			body.velocity = vel;
			jumpReload = jumpWait;
			}
	}
	
	public void LadderUp() {
		    ladderStop = true;
		    collider.attachedRigidbody.useGravity = false;
		    Vector2 vel = body.velocity;
			vel =new Vector2 (0f, 5f);
			body.velocity = vel;
		float yVel = body.velocity.y;
		
		if  (Mathf.Abs(yVel) < 0.03f) {
			legs.rotation = Quaternion.RotateTowards(legs.rotation, Quaternion.identity, maxSpeed * Time.smoothDeltaTime * 10f);
		} else {
			legs.Rotate(0f, 0f, yVel * Time.smoothDeltaTime * -100f);
		}
		
    }
	public void LadderDown() {
		    ladderStop = true;
		    collider.attachedRigidbody.useGravity = false;
		    Vector2 vel = body.velocity;
			vel = new Vector2 (0f,-5f);
			body.velocity = vel;
		float yVel = body.velocity.y;
		
		if  (Mathf.Abs(yVel) < 0.03f) {
			legs.rotation = Quaternion.RotateTowards(legs.rotation, Quaternion.identity, maxSpeed * Time.smoothDeltaTime * 10f);
		} else {
			legs.Rotate(0f, 0f, yVel * Time.smoothDeltaTime * -100f);
		}
		
    }
	public void LadderStop() {
		    Vector2 vel = body.velocity;
			vel = new Vector2 (0f,0f);
			body.velocity = vel;
    }
	public void Run(float direction, float rotation, float posZ) {
		Vector3 vel = body.velocity;
		dir = direction;
		vel.x += force * direction;
		legs.localPosition = new Vector3 (0f,-0.5f,0f);
		if  (Mathf.Abs(vel.x) > maxSpeed) {
			vel.x = maxSpeed * direction;	
		}
		
		body.velocity = vel;
		weapon.localRotation =  Quaternion.Euler(0f, rotation, 0f);
		weapon.localPosition = new Vector3 (0f ,0f, posZ);
		
	}
	public void TunnelRun(float direction, float rotation, float posZ) {
		Vector3 vel = body.velocity;
		dir = direction;
		vel.x += force * direction;
		legs.localPosition = new Vector3 (0f,-0.37f,0.9f);
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
	
	public void DropGranade() {
		if  (granadeReload == 0f && dir != 0f) {
			(Instantiate(granadePrefab, bulletSpawn.position, Quaternion.Euler(0f, 0f, 0f)) as Transform).GetComponent<Rigidbody>().velocity = new Vector3(dir * 12f, 15f, 0f);
			granadeReload = granadeWait;
		}
	}
	public void RocketLauncher () {
		if  (rocketReload == 0f && dir != 0f) {
			(Instantiate(RocketPrefab, bulletSpawn.position, Quaternion.Euler(0f, 0f, 0f)) as Transform).GetComponent<Rigidbody>().velocity = new Vector3(dir * 12f, 15f, 0f);
			rocketReload = rocketWait;
		}
	}
	public bool IsGroundDown {
		get {
			return Physics.Raycast(new Ray(self.position, Vector3.down), 0.8f + (legs.rotation.eulerAngles.z % 90f) / 90f * 0.5f, layerGround );
		}
	}
	
	public bool IsGroundLeft {
		get {
			return Physics.Raycast(new Ray(self.position, Vector3.left), 0.8f, layerGround);
		}
	}
	
	public bool IsGroundRight {
		get {
			return Physics.Raycast(new Ray(self.position, Vector3.right), 0.8f, layerGround);
		}
	}
	public bool IsGroundUp {
		get {
			return Physics.Raycast(new Ray(self.position, Vector3.up), 1f, layerGround );
			
		}
	}
	public bool IsTunnelLeft {
		get {
			return Physics.Raycast(new Ray(self.position + new Vector3(-0.5f,0f,0f), Vector3.up), 1f, layerGround);
			
		}
	}
	public bool IsTunnelRight {
		get {
			return Physics.Raycast(new Ray(self.position + new Vector3(0.5f,0f,0f), Vector3.up), 1f, layerGround);
			
		}
	}
	
	public void Explosion() {
		Vector3 pos = transform.position;
		
		(Instantiate(bloodPrefab, pos, Quaternion.Euler(0f, 0f, Random.value * 360f)) as Transform).GetComponent<ParticleEmitter>().Emit();
		(Instantiate(bloodPrefab, pos, Quaternion.Euler(0f, 0f, Random.value * 360f)) as Transform).GetComponent<ParticleEmitter>().Emit();
		(Instantiate(bloodPrefab, pos, Quaternion.Euler(0f, 0f, Random.value * 360f)) as Transform).GetComponent<ParticleEmitter>().Emit();
		(Instantiate(bloodPrefab, pos, Quaternion.Euler(0f, 0f, Random.value * 360f)) as Transform).GetComponent<ParticleEmitter>().Emit();
		(Instantiate(bloodPrefab, pos, Quaternion.Euler(0f, 0f, Random.value * 360f)) as Transform).GetComponent<ParticleEmitter>().Emit();
		(Instantiate(bloodPrefab, pos, Quaternion.Euler(0f, 0f, Random.value * 360f)) as Transform).GetComponent<ParticleEmitter>().Emit();
		
		Destroy(gameObject);
	}
	
	public void Hit(Vector3 point, Vector3 direction) {
		(Instantiate(bloodPrefab, point, Quaternion.Euler(0f, 0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg)) as Transform).GetComponent<ParticleEmitter>().Emit();
		
		lifes = Mathf.Max(0, lifes - 1);
		
		if  (lifes == 0) {
			Explosion();
		}
	}
	
	void OnCollisionEnter(Collision collision) {
		if  (collision.collider.GetComponent<bullet>() != null) {
			Hit(collision.contacts[0].point, collision.contacts[0].normal);
		}
	}
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "ladder") {
			ladder = true;
		}
	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag == "ladder") {
			ladder = false;
			ladderStop = false;
			time = 0.5f;
		 collider.attachedRigidbody.useGravity = true;
		}

		}
	
	
}