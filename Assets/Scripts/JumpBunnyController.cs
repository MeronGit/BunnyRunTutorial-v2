using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JumpBunnyController : MonoBehaviour {

    private Rigidbody2D myRigidBunny;
    private Animator myAnimator;
    public float bunnyJumpForceShit = 500f;
    private float bunnyHurtTime = -1;
    private Collider2D myCollider;
    public Text scoreText;
    private float startTime;
    private int jumpsLeft = 2;
    public AudioSource jumpSfx;
    public AudioSource deathSfx;

	// Use this for initialization
	void Start () {
        myRigidBunny = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
        startTime = Time.time;
	}

    // Update is called once per frame
    void Update()
    {
        //Let bunny jump only twice!
        if (bunnyHurtTime == -1)
        {
            //Jump with spacebar, unity keybindings
            if (Input.GetButtonUp("Jump") && jumpsLeft > 0)
            {
                if (myRigidBunny.velocity.y < 0)
                {
                    myRigidBunny.velocity = Vector2.zero;
                }
                if (jumpsLeft == 1)
                {
                    myRigidBunny.AddForce(transform.up * bunnyJumpForceShit * 0.75f);
                }
                else
                {
                    myRigidBunny.AddForce(transform.up * bunnyJumpForceShit);
                }
                jumpsLeft--;

                jumpSfx.Play();
            }
            //myAnimator.SetFloat("vVelocity", Mathf.Abs(myRigidBunny.velocity.y));
            myAnimator.SetFloat("vVelocity", myRigidBunny.velocity.y);
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else {
            //two seconds after we collided  +2
            if (Time.time > bunnyHurtTime + 2)
            {
            //Load scene for the currently created scene / 8:39
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            foreach (PrefabsSpawner spawner in FindObjectsOfType<PrefabsSpawner>())
            {
                spawner.enabled = false;
            }
            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                //disable script in the list
                moveLefter.enabled = false;
            }

            bunnyHurtTime = Time.time;
            myAnimator.SetBool("bunnyHurt", true);
            //cancel out any motion of movement bunnny has
            myRigidBunny.velocity = Vector2.zero;
            myRigidBunny.AddForce(transform.up * bunnyJumpForceShit);
            myCollider.enabled = false;
            deathSfx.Play();


        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            jumpsLeft = 2;
        }




    }      //if layermask has enemy, load the scene from the start.
  
}
