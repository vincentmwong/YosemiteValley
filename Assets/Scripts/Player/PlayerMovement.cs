using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float accel;
	public float max_speed;
	public float decay;
	public float dodgeSpeed;
	public float dodgeLength;
	public float coolDown_dodge;
	public float attack1Length;
	public float attack2Length;
	public float attack3Length;
	public float coolDown_attack;
	public Vector2 jumpHeight;
	
	public bool running = false;
	public bool acting = false;


//Setting this sets the animation
	public bool dodging = false;
	public bool jumping = false;
	public bool attack1ing = false;		
	public bool attack2ing = false;
	public bool attack3ing = false;


	private float speed = 0f;
	private PlayerControls playerControls;
	private PlayerFacing playerFacing;

	int direction;
	float scaling = 0.01f;
	Rigidbody2D playerBody;

	IEnumerator Dodge() {
		acting = true;
		dodging = true;
		direction = (playerFacing.dir) ? 1 : -1;
		for (int i=0; i<3; i++){ 			//Leadup (No iframes)
			transform.Translate(direction*(0.5f+(0.5f*(1f/(i+1f))))*dodgeSpeed*scaling,0f,0f);
		 	yield return null;
		}
		for (int i=0; i<10; i++){			//Dodge (All iframes)
			transform.Translate(direction*dodgeSpeed*scaling,0f,0f);
			//Debug.Log(direction*dodgeSpeed*scaling);
		 	yield return null;
		}
		for (int i=0; i<8; i++){			//Cooldown, cooldown time is 10
			transform.Translate(((1f/(i-8f))+(dodgeSpeed+1f))*direction*scaling,0f,0f);
			//Debug.Log(((1f/(i-6f))+(dodgeSpeed+1f))*direction*scaling);
		 	yield return null;
		}
		for (int i=0; i<4; i++){			//Standstill
			yield return null;
		}
		acting = false;
		dodging = false;
		yield break;
	}

	IEnumerator Attack(){
		acting = true;
		attack1ing = true;
		direction = (playerFacing.dir) ? 1 : -1;
		for (int i=0; i<3; i++){ 			//Leadup (No iframes)
			transform.Translate(direction*(0.5f+(0.5f*(1f/(i+1f))))*dodgeSpeed*scaling,0f,0f);
		 	yield return null;
		}
		for (int i=0; i<10; i++){			//Dodge (All iframes)
			transform.Translate(direction*dodgeSpeed*scaling,0f,0f);
			//Debug.Log(direction*dodgeSpeed*scaling);
		 	yield return null;
		}
		for (int i=0; i<8; i++){			//Cooldown, cooldown time is 10
			transform.Translate(((1f/(i-8f))+(dodgeSpeed+1f))*direction*scaling,0f,0f);
			//Debug.Log(((1f/(i-6f))+(dodgeSpeed+1f))*direction*scaling);
		 	yield return null;
		}
		for (int i=0; i<4; i++){			//Standstill
			yield return null;
		}
		acting = false;
		attack1ing = false;
		yield break;
	}

	void Move(){
		if (Input.GetKey(playerControls.moveLeft)){		//left
			speed = speed - accel*scaling;
			running = true;
			dodging = false;
			attack1ing = false;
		} else if (Input.GetKey(playerControls.moveRight)){		//right
			speed = speed + accel*scaling;
			running = true;
			dodging = false;
			attack1ing = false;
		}
		if (!Input.GetKey(playerControls.moveLeft) && !Input.GetKey(playerControls.moveRight)){
			running = false;
		}
		if (jumping == true){
			running = false;
		}
	}

	void Start(){
		playerControls = GetComponent<PlayerControls>();
		playerFacing = GetComponent<PlayerFacing>();
		playerBody = GetComponent<Rigidbody2D>();
	}

	void Update(){
		if (Input.GetKey(playerControls.dodge) && !dodging){
			StartCoroutine("Dodge");
		} else if (Input.GetKey(playerControls.attack) && !attack1ing){
			StartCoroutine("Attack");
		} else if (!acting) {
			Move();
			speed = (speed >= max_speed*scaling) ? max_speed*scaling : speed;
			speed = (speed <= -1*max_speed*scaling) ? -1*max_speed*scaling : speed;
			transform.Translate(speed*Time.deltaTime,0f,0f);
			if (!Input.GetKey(playerControls.moveLeft) && !Input.GetKey(playerControls.moveRight)){
				speed = speed*decay;
			}
			if (speed <= 0.001f && speed >= -0.001f){speed = 0f;}
		}
	}

	/*void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
			jumping = false;
	}

	void Jump(){
		if (Input.GetKeyDown(playerControls.jump) && jumping == false){		//Jump
			playerBody.AddForce(jumpHeight);
			jumping = true;
		}
	}



	// Use this for initialization
	void Start () {
		
		dodgeCoolTimeStamp = Time.time;
		dodgeLenTimeStamp = Time.time;
		attackCoolTimeStamp = Time.time;
		attack1LenTimeStamp = Time.time;
		attack2LenTimeStamp = Time.time;
		attack3LenTimeStamp = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (dodging){
			transform.Translate(direction*dodgeSpeed*scaling,0f,0f);
			if (Time.time >= dodgeLenTimeStamp){
				dodging = false;
				speed = 0.2f*accel*direction;
			}
		} else if (attack3ing && Time.time >= (1.2f)*attack3LenTimeStamp){		//attack3
			attack3ing = false;
		} else if (attack2ing){		//attack2
			if (Input.GetKeyDown(playerControls.attack) && Time.time >= attack2LenTimeStamp){
				attack2ing = false;
				attack3ing = true;
				attack3LenTimeStamp = Time.time + attack3Length;
				attackCoolTimeStamp = Time.time + coolDown_attack;
			} else if (Time.time >= (1.2f)*attack2LenTimeStamp){
				attack2ing = false;
			}
		} else if (attack1ing){		//attack1
			if (Input.GetKeyDown(playerControls.attack) && Time.time >= attack1LenTimeStamp){
				attack1ing = false;
				attack2ing = true;
				attack2LenTimeStamp = Time.time + attack2Length;
				attackCoolTimeStamp = Time.time + coolDown_attack;
			} else if (Time.time >= (1.2f)*attack1LenTimeStamp){
				attack1ing = false;
			}
		} else {	//not attacking or dodging
			if (Input.GetKey(playerControls.dodge) && Time.time >= dodgeCoolTimeStamp && jumping == false && attack1ing == false){		//dodge
				speed = 0;
				direction = (playerFacing.dir) ? 1 : -1;
				running = false;
				dodging = true;
				attack1ing = false;
				dodgeCoolTimeStamp = Time.time + coolDown_dodge;
				dodgeLenTimeStamp = Time.time + dodgeLength;
			} else if (Input.GetKeyDown(playerControls.attack) && Time.time >= attackCoolTimeStamp && jumping == false && dodging == false){		//attack1
				speed = 0;
				running = false;
				dodging = false;
				attack1ing = true;
				attackCoolTimeStamp = Time.time + coolDown_attack;
				attack1LenTimeStamp = Time.time + attack1Length;
			} else {
				Jump();
				Move();
			}
			
		}
	}*/
}
