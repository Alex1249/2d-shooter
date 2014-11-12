using UnityEngine;
using System.Collections;


public class PlayerTwoControls : MonoBehaviour {
	[SerializeField]
	private Actor actor = null;
	

	void Update() {
	
		if (!Input.anyKey && actor.ladderStop) {
			actor.LadderStop();
		}

		if  (Input.GetKeyDown(KeyCode.I) && actor.IsGroundDown) { 
			actor.Jump();
		}
		if  (Input.GetKey(KeyCode.I) && actor.ladder) {
			actor.LadderUp();
		}
		if  (Input.GetKey(KeyCode.K) && actor.ladder) {
			actor.LadderDown();
		}
		
		if  (Input.GetKey(KeyCode.J) && !actor.IsGroundUp) {    
			actor.Run(-1f, 180f,-1.1f);
			
			if  (Input.GetKeyDown(KeyCode.I) && actor.IsGroundLeft) {
				actor.Jump();
			}
		}
		if  (Input.GetKey(KeyCode.J) && (actor.IsTunnelLeft || actor.IsTunnelRight || actor.IsGroundUp)) {    
			actor.TunnelRun(-1f, 180f,-1.1f);
			
		}
		
		if  (Input.GetKey(KeyCode.L) && !actor.IsGroundUp) {
			actor.Run(1f, 0f, 0.6f);
			
			if  (Input.GetKeyDown(KeyCode.I) && actor.IsGroundRight) {
				actor.Jump();
			}
		}
		if  (Input.GetKey(KeyCode.L) && (actor.IsTunnelLeft || actor.IsTunnelRight || actor.IsGroundUp)) {
			actor.TunnelRun(1f, 0f, 0.6f);
		}
		
		if  (Input.GetKey(KeyCode.O)) {
			actor.Shoot();
		}
		
		if  (Input.GetKeyDown(KeyCode.P)) {
			actor.DropGranade();
		}
		if  (Input.GetKeyDown(KeyCode.U)) {
			actor.RocketLauncher();
		}
	}

}