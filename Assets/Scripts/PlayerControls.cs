using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	[SerializeField]
	private Actor actor = null;
	//private Transform self = null;
	//private float time = 0.5f;
	void Awake () {
		//self = transform;
	}

	void Update() {
		
		if (!Input.anyKey && actor.ladderStop) {
			actor.LadderStop();
		}
		
		if  (Input.GetKeyDown(KeyCode.W) && actor.IsGroundDown) {
			actor.Jump();
		}
		if  (Input.GetKey(KeyCode.W) && actor.ladder) {
			actor.LadderUp();
		
		}
		
		if  (Input.GetKey(KeyCode.S) && actor.ladder) {
			actor.LadderDown();
			
		}
		
		if  (Input.GetKey(KeyCode.A) && !actor.IsGroundUp) {
			actor.Run(-1f, 180f, -1.1f);
			
			if  (Input.GetKeyDown(KeyCode.W) && actor.IsGroundLeft) {
				actor.Jump();
			}
		}
		if  (Input.GetKey(KeyCode.A) && (actor.IsTunnelLeft || actor.IsTunnelRight || actor.IsGroundUp)) {
			actor.TunnelRun(-1f, 180f,-1.1f);
			
		}
		
		if  (Input.GetKey(KeyCode.D) && !actor.IsGroundUp) {
			actor.Run(1f, 0f, 0.6f);
			
			if  (Input.GetKeyDown(KeyCode.W) && actor.IsGroundRight) {
				actor.Jump();
			}
		}
		if  (Input.GetKey(KeyCode.D) && (actor.IsTunnelLeft || actor.IsTunnelRight || actor.IsGroundUp)) {
			actor.TunnelRun(1f, 0f, 0.6f);
			
		}
		
		if  (Input.GetKey(KeyCode.C)) {
			actor.Shoot();
		}
		
		if  (Input.GetKeyDown(KeyCode.V)) {
			actor.DropGranade();
		}
		if  (Input.GetKeyDown(KeyCode.B)) {
			actor.RocketLauncher();
		}
	}
	/*
	void FixedUpdate () {
		if (!actor.ladder && actor.time > 0f) {
			actor.time -= Time.smoothDeltaTime;
		    AI.targetPosition = self.position;
			Debug.Log ("SavePos");
		}
	}
	*/
}