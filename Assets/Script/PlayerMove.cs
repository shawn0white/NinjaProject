using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float force_move=50;
    private Animator anim;
    private Rigidbody2D rigidbody2D;
    bool isGround = false;
    bool isWall = false;
    public float jumpVelocity=30.0f;
    Vector3 v;
    float gravityNormal = 10.0f;
    float gravitySlide = 1.0f;
    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }


	// Update is called once per frame
    void Update()
    {
        rigidbody2D = this.GetComponent<Rigidbody2D>();
        Vector2 velocity = rigidbody2D.velocity;
        float h = Input.GetAxis("Horizontal");
        //人物左右移动
        if (h > 0.05f)
        {
            rigidbody2D.AddForce(Vector2.right * force_move);
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (h < -0.05f)
        {
            rigidbody2D.AddForce(-Vector2.right * force_move);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        anim.SetFloat("horizontal", Mathf.Abs(h));
        //
        
        //平地跳跃
        if (isGround && Input.GetKeyDown(KeyCode.Space)) {
            velocity.y = jumpVelocity;
            rigidbody2D.velocity = velocity;
        }
        anim.SetBool("isGround", isGround);
        anim.SetFloat("Y_velocity", velocity.y);
        //

        //滑行
        if (isWall && !isGround) {        
            rigidbody2D.gravityScale = gravitySlide;
            transform.localScale = v;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpVelocity;
                rigidbody2D.velocity = velocity;
                transform.localScale = v;
            }
        }
        if (!isWall) {
            rigidbody2D.gravityScale = gravityNormal;
        }
        
        anim.SetBool("isWall", isWall);
        //
    }

    //接触碰撞体
    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.tag == "Ground") {
            isGround = true;            
        }
        if (col.collider.tag == "RightWall") {
            isWall = true;
            if (!isGround) {
                v = new Vector3(1, 1, 1);
            }
        }
        if (col.collider.tag == "Enemy")
        {
            transform.position = new Vector3(-21, 14, 0);
        }
    }
    //

    //离开碰撞体
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.tag == "Ground")
        {
            isGround = false;           
        }
        if (col.collider.tag == "RightWall")
        {
            isWall = false;
        }
    }
    //
}
