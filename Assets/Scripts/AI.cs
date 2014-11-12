using UnityEngine;

public class AI : MonoBehaviour {
	[SerializeField]
	private Actor actor = null;
	[SerializeField]
	private float viewDistance = 15f, pursuitTime = 10f,researchTime = 3f, pursuitOne = 1f, pursuitTwo = 2f, distanceToFight = 7f, PlayerPursuit=1f, Pause = 0.5f, Select; 	
	private Transform self = null, player = null, playerTwo = null;
	private  Vector3 targetPosition;
	// Режим двух игроков
	
	public static bool playerTwoActive = true;
	
	private float viewTime = 0f, researchPlayer = 1.6f, searchTime = 0f;
	// Для игрока 1																								// Для игрока 2
	private int  layerPlayerRay = 0, layerGroundRay = 0, layerEnemyRay = 0,layerPlayerBulletRay = 0,layerPlayerTwoBulletRay = 0,layerGranadePlayerRay =0,layerGranadePlayerTwoRay = 0, layerEnemyBulletRay = 0, layerPlayer = 0, layerPlayerBullet = 0, layerPlayerTwoRay = 0,  layerPlayerTwo = 0, layerPlayerTwoBullet = 0, layerGranadePlayer = 0, layerGranadePlayerTwo = 0;
	
    private bool IsGroundDownLeft {
		get {
			return Physics.Raycast(new Ray(self.position + new Vector3(-1f,0f,0f), Vector3.down), 5f, layerGroundRay );
			
		}
	}
	private bool IsGroundDownRight {
		get {
			return Physics.Raycast(new Ray(self.position + new Vector3(1f,0f,0f), Vector3.down), 5f, layerGroundRay );
			
		}
	}
	Vector3 dir;
	private bool DontObject {
		get {
			return Physics.Raycast(new Ray(self.position, dir), 3f, layerPlayerRay  | layerPlayerTwoRay | layerGroundRay | layerEnemyRay);
			
		}
	}
	private bool Danger {
		get {
			return  Physics.SphereCast(new Ray(self.position, dir), 3f, 3f, layerPlayerBulletRay  | layerPlayerTwoBulletRay | layerGranadePlayerRay | layerGranadePlayerTwoRay | layerEnemyBulletRay);	
		}
	}

	
	private void View() {
	//	if  (player == null) {
	//	return;
	//	}
		
		Vector3 pos = self.position;
		dir = actor.dir > 0f ? Vector3.right : Vector3.left;
		Vector2 dir2 = actor.dir > 0f ? new Vector2 (3f,0.5f) : new Vector2(-3f,0.5f);
		Vector2 dir3 = actor.dir > 0f ? new Vector2 (3f,-0.5f) : new Vector2(-3f,-0.5f);
		viewTime = Mathf.Max(0f, viewTime - Time.smoothDeltaTime);
		
		RaycastHit hit;
		
		// Первый игрок
		if  (Physics.Raycast(new Ray(pos, dir), out hit, viewDistance, layerPlayerRay  | layerPlayerTwoRay | layerGroundRay |  layerPlayerBulletRay | layerPlayerTwoBulletRay | layerGranadePlayerRay | layerGranadePlayerTwoRay)) {
			if  (hit.collider.gameObject.layer == layerPlayer | hit.collider.gameObject.layer == layerPlayerBullet | hit.collider.gameObject.layer == layerGranadePlayer) {
			    PlayerPursuit = pursuitOne ;
				viewTime = pursuitTime;
				searchTime =0f;
				targetPosition = player.position;
				//Debug.Log(targetPosition);
			} 
			// Второй
			
			if  (hit.collider.gameObject.layer == layerPlayerTwo | hit.collider.gameObject.layer == layerPlayerTwoBullet | hit.collider.gameObject.layer == layerGranadePlayerTwo) {
			    PlayerPursuit = pursuitTwo ;
				viewTime = pursuitTime;
				searchTime =0f;
				targetPosition = playerTwo.position;
				//Debug.Log(targetPosition);
			
				}
				
			}	
		if  (Physics.Raycast(new Ray(pos, dir2), out hit, viewDistance, layerPlayerRay  | layerPlayerTwoRay | layerGroundRay |  layerPlayerBulletRay | layerPlayerTwoBulletRay | layerGranadePlayerRay | layerGranadePlayerTwoRay)) {
			if  (hit.collider.gameObject.layer == layerPlayer | hit.collider.gameObject.layer == layerPlayerBullet | hit.collider.gameObject.layer == layerGranadePlayer) {
			    Transform tPlayer = hit.collider.transform;
				targetPosition = tPlayer.position;
				
				PlayerPursuit = pursuitOne ;
				viewTime = pursuitTime;
				searchTime =0f;
				targetPosition = player.position;
				//Debug.Log(targetPosition);
			} 
			// Второй
			
			if  (hit.collider.gameObject.layer == layerPlayerTwo | hit.collider.gameObject.layer == layerPlayerTwoBullet | hit.collider.gameObject.layer == layerGranadePlayerTwo) {
			    PlayerPursuit = pursuitTwo ;
				viewTime = pursuitTime;
				searchTime =0f;
				targetPosition = playerTwo.position;
				//Debug.Log(targetPosition);
			
				}
				
			}
		if  (Physics.Raycast(new Ray(pos, dir3), out hit, viewDistance, layerPlayerRay  | layerPlayerTwoRay | layerGroundRay |  layerPlayerBulletRay | layerPlayerTwoBulletRay | layerGranadePlayerRay | layerGranadePlayerTwoRay)) {
			if  (hit.collider.gameObject.layer == layerPlayer | hit.collider.gameObject.layer == layerPlayerBullet | hit.collider.gameObject.layer == layerGranadePlayer) {
			    PlayerPursuit = pursuitOne ;
				viewTime = pursuitTime;
				searchTime = 0f;
				targetPosition = player.position;
				//Debug.Log(targetPosition);
			} 
			// Второй
			
			if  (hit.collider.gameObject.layer == layerPlayerTwo | hit.collider.gameObject.layer == layerPlayerTwoBullet | hit.collider.gameObject.layer == layerGranadePlayerTwo) {
			    PlayerPursuit = pursuitTwo ;
				viewTime = pursuitTime;
				searchTime = 0f;
				targetPosition = playerTwo.position;
				//Debug.Log(targetPosition);
			
				}	
			}
		}

	private void Shoot() {
		actor.Shoot();
	}
	
	private void PursuitRun(Vector3 selfPos, Vector3 playerPos, Vector3 dir) {
		// Поднимаемся по леснице 
		if (actor.ladder &&  ((playerPos.y-selfPos.y) >=1) && viewTime > 0f) {
			if (!actor.IsTunnelLeft && !actor.IsTunnelRight) {
				actor.LadderUp();
			} else {
				if (actor.IsTunnelLeft) {
				actor.Run(1, 0f, 0.6f);
				} else { if (actor.IsTunnelRight){
						actor.Run(-1, 0f, 0.6f);
					}
				}
			}
		} else { 
			if (dir.x > 0 && !actor.IsGroundUp){
		actor.Run(dir.x, 0f, 0.6f);
		} else {  if (dir.x < 0 && !actor.IsGroundUp)
			actor.Run(dir.x, 180f, -1.1f);
			}
		}
		// Проходим через туннель в один кубик
		if  (dir.x < 0 && (actor.IsTunnelLeft || actor.IsTunnelRight || actor.IsGroundUp) && !actor.ladder) {
			actor.TunnelRun(-1f, 180f,-1.1f);
			
		} else { if (dir.x > 0  && (actor.IsTunnelLeft || actor.IsTunnelRight || actor.IsGroundUp) && !actor.ladder)
			actor.TunnelRun(1f, 0f, 0.6f);
			
		}
		// Прыгаем через пропасть во время преследования 
		if ((!IsGroundDownLeft || !IsGroundDownRight) && actor.IsGroundDown){	
						
					actor.Jump ();		
				}
		if  (dir.x < 0f) {
			
			
			if  (actor.IsGroundLeft) {
				actor.Jump();
			}
		} else {
			
	if  (actor.IsGroundRight) {
				
				actor.Jump();
			}
		}
	}
	
	
	private void Pursuit() {

		if  ((viewTime > 0f && player!=null && PlayerPursuit == 1) || (viewTime > 0f && playerTwo!=null && PlayerPursuit == 2)) {
			
			if (Danger && actor.IsGroundDown) { /*Debug.Log ("Опасная зона");*/ actor.Jump (); }
			Vector3 playerPos = PlayerPursuit < 2 && player!=null ? player.position : playerTwo.position;
			Vector3 selfPos = self.position;
			float dist = Vector3.Distance(playerPos, selfPos);
			float distTarget = Vector3.Distance(targetPosition,selfPos);
			Vector3 dir = targetPosition.x > selfPos.x ? Vector3.right : Vector3.left;
				if  (Vector3.Distance(playerPos, self.position) > viewDistance * 2f) {
			viewTime = 0f;
		}
			
			if  (dist <= distanceToFight) {
				Vector3 curDir = actor.dir > 0f ? Vector3.right : Vector3.left;
					
				if  (curDir == dir) {
				if (Mathf.RoundToInt(playerPos.y - selfPos.y) == 0 && actor.IsGroundDown) {
							
						if (Pause > 0f){
							Pause -= Time.smoothDeltaTime;
						} else { 
							Select = Random.value;
							
							if  (Select < 0.1f) {
								actor.DropGranade();
							} else { 
								Shoot(); 
							}
							
							if (Select > 0.9f && !DontObject) {
								actor.RocketLauncher();
							}
						}
						
					} else { 
							PursuitRun(selfPos, playerPos, dir);
						}
						
				} else {  
					PursuitRun(selfPos, playerPos, dir);
						}
			} else {  
					if ( distTarget>1.5f && actor.IsGroundDown) {
					if (!actor.ladder) {
					PursuitRun(selfPos, targetPosition, dir);
						} else {PursuitRun(selfPos, playerPos, dir);}
					} else { if ( distTarget<1.5f && dist > 1.5f  && actor.IsGroundDown) {
							searchTime = researchTime;
						}	
					}	
				}
			}	   	 
		}
	private void SearchPlayer () {
		//---------------------------------- Потеря игрока из виду-------------------------------------
							if (searchTime > 0f) {
			//Debug.Log("Потеря игрока");
			Vector3 selfPos = self.position;
			float distTarget = Vector3.Distance(targetPosition,selfPos);
			if ( (distTarget<2 | Mathf.RoundToInt(targetPosition.x - selfPos.x) == 0) && actor.IsGroundDown) {
								searchTime -= Time.smoothDeltaTime;
								if (researchPlayer>0.8 ) {
										researchPlayer -= Time.smoothDeltaTime;
										actor.dir = 1f;
										actor.weapon.localRotation =  Quaternion.Euler(0f, 0f, 0f);
										actor.weapon.localPosition = new Vector3 (0f ,0f, 0.6f);
										Debug.Log ("вправо");
									} else { if (researchPlayer>0) {
												researchPlayer -= Time.smoothDeltaTime;
												actor.dir = -1f;
												actor.weapon.localRotation =  Quaternion.Euler(0f, 180, 0f);
												actor.weapon.localPosition = new Vector3 (0f ,0f, -1.1f);
												Debug.Log ("влево");
											
										} else { researchPlayer=1.7f; }
										} 
									}
								}
							}
	private float patrolTime = 0f;
	private float patrolType = 0f;
	
	private void Patrol() {
		patrolTime = Mathf.Max(0f, patrolTime - Time.smoothDeltaTime);

		if  (viewTime == 0f && searchTime <= 0f) {
			if  (patrolTime == 0f || (patrolType > 0f && actor.IsGroundRight) || (patrolType < 0f && actor.IsGroundLeft)) {
				patrolTime = Random.value + Random.value + 1f;
				patrolType = Mathf.Round((Random.value - 0.5f) * 2f);
			}
			
			if  (patrolType != 0f) {
				if  (Physics.Raycast(new Ray(self.position + new Vector3(patrolType * 1.75f, 0f, 0f), Vector3.down), 0.8f, layerGroundRay)) {
					if (patrolType > 0){
					actor.Run(patrolType, 0f, 0.6f);
					} else { 
						actor.Run(patrolType, 180f, -1.1f);
					}
				}
			}
		}
	}
	
	void Awake() {
			self = transform;
			//layerLadderRay = 1 << LayerMask.NameToLayer("ladder");
			layerGroundRay = 1 << LayerMask.NameToLayer("ground");
			layerEnemyRay = 1 << LayerMask.NameToLayer("enemy");
			layerEnemyBulletRay = 1 << LayerMask.NameToLayer("enemybullet");
			// Игрок 1
			player = GameObject.Find("player").transform;
			layerPlayerRay = 1 << LayerMask.NameToLayer("player");		
			layerPlayer =  LayerMask.NameToLayer("player");
			layerPlayerBullet = LayerMask.NameToLayer("bulletPlayer");
			layerPlayerBulletRay = 1 << LayerMask.NameToLayer("bulletPlayer");
		    layerGranadePlayerRay = 1 << LayerMask.NameToLayer("granadePlayer");
			layerGranadePlayer = LayerMask.NameToLayer("granadePlayer");
			
	}
	void Start () {
		// Игрок 2
			if (playerTwoActive){
			playerTwo = GameObject.Find("playerTwo").transform;		
			layerPlayerTwoRay = 1 << LayerMask.NameToLayer("playerTwo");		
			layerPlayerTwo  =  LayerMask.NameToLayer("playerTwo");
			layerPlayerTwoBullet = LayerMask.NameToLayer("bulletPlayerTwo");
			layerPlayerTwoBulletRay = 1 << LayerMask.NameToLayer("bulletPlayerTwo");
			layerGranadePlayerTwoRay = 1 << LayerMask.NameToLayer("granadePlayerTwo");
			layerGranadePlayerTwo = LayerMask.NameToLayer("granadePlayerTwo");
		}
	}
	void Update() {
		//Debug.Log(reserchPlayer);
		View();
		Pursuit();
		Patrol();
		SearchPlayer ();
	}

	void OnCollisionEnter(Collision collision) {
		int l = collision.collider.gameObject.layer;
		// Игрок 1
		if  (l == layerPlayer | l == layerPlayerBullet | l == layerGranadePlayer) {
			PlayerPursuit = pursuitOne;
			viewTime = pursuitTime;
			targetPosition = player.position;
			
		}
		if  (l == layerPlayerTwo | l == layerPlayerTwoBullet | l == layerGranadePlayerTwo) {
			PlayerPursuit = pursuitTwo;
			viewTime = pursuitTime;
			targetPosition = playerTwo.position;
			
		}
		
	}
	
	void OnBecameVisible() {
        enabled = true;
    }
	void OnBecameInvisible() {
        enabled = false;
    }
}