using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public GameObject ObjectOfFollow;
	public float cameraBackwardDistance = -1.5f;

	Transform target;
	Vector3 velocity = Vector3.zero;
	float dampTime = 0.15f;
	GameObject Camera;

	// Use this for initialization
	void Start () {
		ObjectOfFollow = GameObject.Find("ObjectOfFollow");
		target = ObjectOfFollow.transform;
		Camera = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (target){
			transform.position = Vector3.SmoothDamp(transform.position, new Vector3(target.position.x,target.position.y,transform.position.z), ref velocity, dampTime);
		}
	}

	//Shake coroutine
	public IEnumerator Shake(float duration, float magnitude) {
		
		float elapsed = 0.0f;
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;		  
			
			float percentComplete = elapsed / duration;		 
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			this.transform.position = new Vector3(this.transform.position.x+x, this.transform.position.y+y, this.transform.position.z);
				
			yield return null;
		}
	}
}
