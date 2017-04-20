using UnityEngine;
using System.Collections;

public class CameraObjectFollowCenter : MonoBehaviour {

	Vector3 objectFollow;
	PlayerFacing playerFacing;
	Vector2 playerOffset;

	// Use this for initialization
	void Start () {
		playerFacing = GameObject.Find("Player").GetComponent<PlayerFacing>();
	}
	
	// Update is called once per frame
	void Update () {
		objectFollow = transform.parent.gameObject.transform.position;
		if (transform.parent.name == "Player"){
			int direction = (playerFacing.dir) ? 1 : -1;
			playerOffset = new Vector2(direction*0.2f,0.1f);
		} else if (playerOffset != Vector2.zero){
			playerOffset = Vector2.zero;
		}
		transform.position = new Vector3(objectFollow.x+playerOffset.x, objectFollow.y+playerOffset.y, objectFollow.z);
	}
}
