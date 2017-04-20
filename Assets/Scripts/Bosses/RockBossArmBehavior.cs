using UnityEngine;
using System.Collections;

public class RockBossArmBehavior : MonoBehaviour {

	public bool active;
	public float hover_height = 1f;
	public float grounded_time = 2f;
	public GameObject player;
	public CameraMovement main_camera_script;

	public enum BossState{Grounded,Float,Chasing,Falling,Rising};
	Rigidbody2D rb;
	public BossState bossState;
	float float_time = 0f;
	float hover_time = 0f;
	float ground_time = 0f;
	float shake_time = 0.3f;
	float shake_timestamp = 0f;
	float first_rising_time = 0.05f;
	float first_rising_timestamp = 0f;
	Vector2 original_position;
	float player_horizontal;

	void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Ground"){
        	main_camera_script.StartCoroutine(main_camera_script.Shake(0.3f,0.02f));
        	ground_time = Time.time + grounded_time;
        	bossState = BossState.Grounded; //when hit ground, set boss state to grounded
        }
    }

	IEnumerator Shake(float duration, float magnitude) {
		
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
				
			yield return elapsed;
		}
		this.transform.position = this.transform.position;
	}

	void Float(){
		//pause
		if (rb.isKinematic == false){
			rb.isKinematic = true;
		}
		if (float_time < Time.time){
			bossState = BossState.Chasing;
		}
	}

	void Chasing(){
		player_horizontal = player.transform.position.x;
		if (player_horizontal > transform.position.x){
			transform.position += transform.right * Time.deltaTime * 0.9f; //move horizontal toward player
		} else if (player_horizontal < transform.position.x){
			transform.position -= transform.right * Time.deltaTime * 0.9f; //move horizontal toward player
		}
		if (Mathf.Abs(this.transform.position.x - player_horizontal) < 0.1f){ //when close enough, set boss state to falling
			bossState = BossState.Falling;
			shake_timestamp = Time.time + shake_time;
			original_position = this.transform.position;
		}
	}

	void Falling(){
		if (shake_timestamp > Time.time){	//shake
			float magnitude = 0.03f;

			float percentComplete = (shake_timestamp-Time.time) / shake_time;		 
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			this.transform.position = new Vector3(this.transform.position.x+x, this.transform.position.y+y, this.transform.position.z);
		} else {
			if (rb.isKinematic == true){//fall
				this.transform.position = original_position;
				rb.isKinematic = false;
				rb.WakeUp();
			}
		}
	}

	void Grounded(){
		//pause
		if (ground_time < Time.time){
			bossState = BossState.Rising;
			first_rising_timestamp = Time.time + first_rising_time;
			original_position = this.transform.position;
		}
	}

	void Rising(){
		if (rb.isKinematic == false){
			rb.isKinematic = true;
		}
		if (first_rising_timestamp > Time.time){ //shoot up
			transform.Translate(0f,(this.transform.position.y-original_position.y)*4f*Time.deltaTime,0f);
		} else if (first_rising_timestamp + first_rising_time < Time.time){
			player_horizontal = player.transform.position.x;
			if (player_horizontal > transform.position.x){
				transform.position += new Vector3(Mathf.Abs(hover_height-(hover_height-this.transform.position.y))*2f,2f,0f) * Time.deltaTime * 0.7f; //slow rise up to hover height
			} else if (player_horizontal < transform.position.x){
				transform.position += new Vector3(Mathf.Abs(hover_height-(hover_height-this.transform.position.y))*-2f,2f,0f) * Time.deltaTime * 0.7f; //slow rise up to hover height
			}
			
			if ((hover_height - transform.position.y) <= 0.05f){ //Set boss state to Idle
				bossState = BossState.Float;
				float_time = Time.time + hover_time;
				rb.isKinematic = true;
			}		
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		player = GameObject.Find("Player");
		main_camera_script = GameObject.Find("Main Camera").GetComponent<CameraMovement>();
		bossState = BossState.Float;
		float_time = Time.time + hover_time;
	}

	// Update is called once per frame
	void Update () {
		if (active){
			if (bossState==BossState.Float){
				Float();
			} else if (bossState==BossState.Chasing){
				Chasing();
			} else if (bossState==BossState.Falling){
				Falling();
			} else if (bossState==BossState.Grounded){
				Grounded();
			} else if (bossState==BossState.Rising){
				Rising();
			}
		}
	}
}
