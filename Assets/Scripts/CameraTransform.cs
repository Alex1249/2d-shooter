using UnityEngine;
using System.Collections;

public class CameraTransform : MonoBehaviour {
 
    private Transform self=null, player=null, playerTwo=null;
	
	public  bool TwoPlayerActive = true;
	private float  CamPosX, CamPosY;
	
	
	void Awake () {
		Application.targetFrameRate = 60;
		self = transform;
		player = GameObject.Find("player").transform;

	
	}
 void Start () {
		if (TwoPlayerActive == true){
			playerTwo = GameObject.Find("playerTwo").transform;
		}
	}
    void FixedUpdate() {
		if (player == null && playerTwo == null) {
			Application.LoadLevel(Application.loadedLevel);
			return;
		}
	
		if (!TwoPlayerActive) {
			if (player != null && playerTwo == null){
			self.position = player.position + new Vector3 (0,0,-8);
				
			camera.orthographicSize = 12;
			} else {
				self.position = playerTwo.position + new Vector3 (0,0,-8);
				camera.orthographicSize = 12;
			}
		} else { if (player !=null && playerTwo != null){
	
		         self.position = ((playerTwo.position - player.position)/2.0f) + player.position + new Vector3 (0,0,-8);
				 float Distance = Vector3.Distance (player.position,playerTwo.position);
				 if (Distance > 24f) {
				 camera.orthographicSize = Distance/2f;
				}
			} else { if (player != null && playerTwo ==null) {
					if (camera.orthographicSize > 12){ 
					camera.orthographicSize -= Time.smoothDeltaTime*2;
				} else {
						TwoPlayerActive = false;
					}
				 if  (Mathf.RoundToInt(player.position.x - self.position.x) != 0 ){
					if (self.position.x < player.position.x) {
					 CamPosY = self.position.y;
					 CamPosX = self.position.x;
					 CamPosX += Time.smoothDeltaTime*8;
					 self.position = new Vector3(CamPosX,CamPosY,-8);
					} else { if (self.position.x > player.position.x) { 
						CamPosY = self.position.y;
						CamPosX = self.position.x;
					    CamPosX -= Time.smoothDeltaTime*8;
					    self.position = new Vector3(CamPosX,CamPosY,-8);	
							}
						}
					} 
				if  (Mathf.RoundToInt(player.position.y - self.position.y) != 0){
					if (self.position.y < player.position.y) {
					 CamPosY = self.position.y;
					 CamPosX = self.position.x;
					 CamPosY += Time.smoothDeltaTime*8;
					 self.position = new Vector3(CamPosX,CamPosY,-8);
					} else { if (self.position.y > player.position.y) { 
						CamPosY = self.position.y;
						CamPosX = self.position.x;
						CamPosY -= Time.smoothDeltaTime*6;
					    self.position = new Vector3(CamPosX,CamPosY,-8);	
						}
					}
				} 
				
			} else {
				if (camera.orthographicSize >12){
					camera.orthographicSize -= Time.smoothDeltaTime*2;
				} else {
						TwoPlayerActive = false;
					}
				 if  (Mathf.RoundToInt(playerTwo.position.x - self.position.x) != 0){
					if (self.position.x < playerTwo.position.x) {
					 CamPosY = self.position.y;
					 CamPosX = self.position.x;
					 CamPosX += Time.smoothDeltaTime*8;
					 self.position = new Vector3(CamPosX,CamPosY,-8);
					} else { if (self.position.x > playerTwo.position.x) {
							CamPosY = self.position.y;
						    CamPosX = self.position.x;
						    CamPosX -= Time.smoothDeltaTime*8;
						    self.position = new Vector3(CamPosX,CamPosY,-8);
						}
					}
				} 
					if  (Mathf.RoundToInt(playerTwo.position.y - self.position.y) != 0){
							if (self.position.y < playerTwo.position.y) {
							CamPosY = self.position.y;
							CamPosX = self.position.x;
							CamPosY += Time.smoothDeltaTime*8;
							self.position = new Vector3(CamPosX,CamPosY,-8);
					}  else {
					if (self.position.y > playerTwo.position.y) {
						CamPosY = self.position.y;
						CamPosX = self.position.x;
					    CamPosY -= Time.smoothDeltaTime*8;
					    self.position = new Vector3(CamPosX,CamPosY,-8);
							}
						}
					}
				} 				
			}
		}
	}
}
	
	