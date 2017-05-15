using UnityEngine;
using System.Collections;

public class Award : MonoBehaviour {

    public float speed = 100.0f;

	// Update is called once per frame
	void Update () {
        transform.Rotate(-Vector3.forward * speed * Time.deltaTime);
	}
    public void OnTriggerEnter2D(Collider2D col) {
        
        if (col.tag == "Player") {
            //Debug.Log("pong");
            AudioManager._instance.PlayCollectible();
            Destroy(this.gameObject);
        }
    }
}
