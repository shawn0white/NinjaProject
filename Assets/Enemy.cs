using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    private Transform player;
    private Animator anim;
    public float speed=5;
    public float attackDistance = 25;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < attackDistance)
        {
            anim.SetBool("Run", true);
            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1,1,1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            Vector3 dir = player.position - transform.position;
            transform.position = dir.normalized * speed * Time.deltaTime + transform.position;
        }
        else
        {
            anim.SetBool("Run", false);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Player")
        {
            transform.position = new Vector3(21, 2, 0);
        }

    }

}
