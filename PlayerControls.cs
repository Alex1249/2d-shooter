using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	
	[SerializeField]
	private Actor actor = null;

	void Update() {

		if  (Input.GetKeyDown(KeyCode.W) && actor.IsGroundDown) {
			actor.Jump();

		}
		
		if  (Input.GetKey(KeyCode.A) && !actor.IsGroundUp) {
			actor.Run(-1f, 180f, -1.1f);
			if  (Input.GetKeyDown(KeyCode.W) && actor.IsGroundLeft) {
				actor.Jump();
			}
		}
		
		if  (Input.GetKey(KeyCode.D) && !actor.IsGroundUp) {
			actor.Run(1f, 0f, 0.6f);
			
			if  (Input.GetKeyDown(KeyCode.W) && actor.IsGroundRight) {
				actor.Jump();
			}
		}

		if  (Input.GetKey(KeyCode.C)) {
			actor.Shoot();
		}
	}
}