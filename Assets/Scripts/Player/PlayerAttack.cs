using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other) {
        other.gameObject.SendMessage("DealDamage", null, SendMessageOptions.DontRequireReceiver);
    }
}
