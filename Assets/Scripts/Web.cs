using UnityEngine;
using System.Collections;

public class Web : MonoBehaviour {
 
private int connectSQL; 
private string message, urlPhoto = null, nameUser = null, auth_key, vk_id, viewer_id; 
	
Rect nameRect, ShopButton, PhotoBox, windowRect;

Texture photo = null; 

     
	void OnGUI () { 	
		
		windowRect = GUI.Window(0, windowRect, DoMyWindow, "Профиль");
    }
	
    void DoMyWindow(int windowID) {
		GUI.Box(PhotoBox, photo);
		GUI.Label (nameRect, nameUser); 
		if (GUI.Button(ShopButton, "Купить")){
	    Application.ExternalCall("shoping", ""); 
		}  
    }
	void Awake () {
		  Application.ExternalCall("Incilization", ""); 
		  PhotoBox = new Rect(10, 20, 100, 100);
		  ShopButton = new Rect (120, 100, 70, 20);
		  nameRect = new Rect (10, 120, 200, 20);
		  windowRect = new Rect(10, 10, 200, 150);
}
	public IEnumerator DownLoadeName () {
			message = "http://globelsoft.com/demovk/ExampleLingPlay/NameUser.php?viewer_id="+viewer_id;
			WWW nameUser_GET = new WWW(message);
		    yield return nameUser_GET ; 
			nameUser = nameUser_GET.text;
	}
	public IEnumerator DownLoadePhotoUrl () {
			message = "http://globelsoft.com/demovk/ExampleLingPlay/AvatarDownload.php?viewer_id="+viewer_id;
			WWW url_GET = new WWW(message);
		    yield return url_GET ; 
			urlPhoto = url_GET.text;
	}
	public IEnumerator DownLoadePhoto () {
	        WWW www = new WWW(urlPhoto);
            yield return www;
            photo = www.texture;		
	}

	void FixedUpdate () {
	if (nameUser == null){
        StartCoroutine ("DownLoadeName");
		}
	if (urlPhoto == null){
		StartCoroutine ("DownLoadePhotoUrl");
		}
	    StartCoroutine ("DownLoadePhoto");
	} 

	void _viewer_id (string v_id ) { 
			viewer_id = v_id ; 
		} 
	  
	
	void _auth_key (string v_key ) { 
			auth_key = v_key ; 
		} 
	}
