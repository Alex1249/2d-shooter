using UnityEngine;

public class ground : MonoBehaviour {
	[SerializeField]
	private Transform fxPrefab = null;
	
	private int lifes = 0;
	private int layerGround = 0;
	
	private void CreateFX(Vector3 point) {
		Transform fx = (Transform)Instantiate(fxPrefab, point, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
		MeshFilter mf = fx.GetComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		Mesh sm = mf.sharedMesh;
		
		Vector3[] pos = sm.vertices;
		Vector2[] uv = new Vector2[sm.vertexCount];
		
		for (int i = 0; i < sm.vertexCount; i++) {
			Vector3 p = fx.TransformPoint(pos[i]);
			uv[i] = new Vector2(p.x * 0.25f, p.y * 0.25f);
		}
		
		mesh.vertices = pos;
		mesh.uv = uv;
		mesh.triangles = sm.triangles;
		
		mesh.RecalculateBounds();
		
		mf.sharedMesh = mesh;
	}
	
	private void CreateBulletEffect(Vector3 point) {
		CreateFX(point + new Vector3((Random.value - 0.5f) * 0.2f, (Random.value - 0.5f) * 0.2f, 0f));
		CreateFX(point + new Vector3((Random.value - 0.5f) * 0.2f, (Random.value - 0.5f) * 0.2f, 0f));
		
		if  (Random.value > 0.5f) {
			CreateFX(point + new Vector3((Random.value - 0.5f) * 0.2f, (Random.value - 0.5f) * 0.2f, 0f));
		}
	}
	
	private void CreateDestroyEffect(Vector3 point) {
		CreateFX(point + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0f));
		CreateFX(point + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0f));
		CreateFX(point + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0f));
		CreateFX(point + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0f));
		CreateFX(point + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0f));
		CreateFX(point + new Vector3((Random.value - 0.5f), (Random.value - 0.5f), 0f));
	}
	
	private bool killed = false;
	private int layerPlayer = 0, layerEnemy = 0, layerPlayerTwo = 0, layerPlayerGranade = 0, layerPlayerTwoGranade = 0;
	
	void Awake() {
		layerGround = 1 << LayerMask.NameToLayer("ground");
		layerPlayer = LayerMask.NameToLayer("player");
		layerPlayerGranade = LayerMask.NameToLayer("granadePlayer");
		layerPlayerTwo = LayerMask.NameToLayer("playerTwo");
		layerPlayerTwoGranade = LayerMask.NameToLayer("granadePlayerTwo");
		layerEnemy = LayerMask.NameToLayer("enemy");
		lifes = Random.Range(6, 8);
	}
	
	public void Kill(float chance){
		if  (killed) {
			return;
		}
		
		killed = true;
		
		if  (Random.value < chance) {
			RaycastHit hit;
			
			if  (Physics.Raycast(new Ray(transform.position, Vector3.up), out hit, 1f, layerGround)) {
				hit.collider.GetComponent<ground>().Kill(chance * 0.8f);
			}
		}
		
		CreateDestroyEffect(transform.position);
		Destroy(gameObject);
	}
	
	void OnCollisionEnter(Collision collision) {
		int l = collision.gameObject.layer;
		
		if  (l == layerPlayer || l == layerEnemy || l == layerPlayerTwo || l == layerPlayerGranade || l == layerPlayerTwoGranade) {
			return;
		}
		
		lifes--;
		
		CreateBulletEffect(collision.contacts[0].point);
		
		if  (lifes <= 0) {
			Kill(0.5f);
		}
	}
	
}