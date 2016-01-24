using UnityEngine;
using System.Collections;

namespace Vertical
{
    public enum GroundReference { left, right};

    public class PlayerMovement : MonoBehaviour {
        public PlayerMovement instance = null;
        public float m_Speed = 10;

        private float vAxis;
        private float hAxis;
        public GroundReference ground;
        public bool has_land;
        public bool has_deploy;
        public bool gravity_change;
        public float deployRange;

        private Transform ship_base;
        private Rigidbody2D rb2d;
        RaycastHit2D hit;
		
        void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(this);
            
        }

	    // Use this for initialization
	    void Start () {
            rb2d = GetComponent<Rigidbody2D>();
            Physics2D.gravity = ground == GroundReference.left ? new Vector2(-1.0f, 0) : new Vector2(1.0f, 0);
            if (ground == GroundReference.left)
                hit = Physics2D.Raycast(transform.position, Vector2.left);
            else if (ground == GroundReference.right)
                hit = Physics2D.Raycast(transform.position, Vector2.right);

	    }
	
	    // Update is called once per frame
	    void FixedUpdate () {
            
            hAxis = Input.GetAxis("Horizontal");
            if (hAxis != 0) {
                if (hAxis < -0.3f & !gravity_change) {
                    ground = GroundReference.left;
                    gravity_change = true;
                    Debug.Log("goin to left");
                } else if (hAxis > 0.3f & !gravity_change){
                    ground = GroundReference.right;
                    gravity_change = true;
                    Debug.Log("goin to right");
                }
            }

            

            bool attack = Input.GetButtonDown("Fire1");
            float xMove = 0;
            switch (ground)
            {
                case GroundReference.left:
                    xMove = 10 * m_Speed * Time.fixedDeltaTime;
                    hit = Physics2D.Raycast(transform.position, Vector2.left);
                    break;
                case GroundReference.right:
                    xMove = -10 * m_Speed * Time.fixedDeltaTime;
                    hit = Physics2D.Raycast(transform.position, Vector2.right);
                    break;
                default:
                    break;
            }
            if (attack) {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(xMove, 0), ForceMode2D.Impulse);
                //Debug.Log(rb2d.velocity);
            }
            else {
                
            }
            
            Physics2D.gravity = ground==GroundReference.left? new Vector2(-1.0f, 0): new Vector2(1.0f, 0);

            if (hit.collider != null & hit.distance < deployRange) {
                if (!has_land) {
                    Debug.Log("landing");
                }

            }
            if (hit.collider != null & hit.distance > deployRange) {
                //Debug.Log("On Air");
                //Debug.Log(hit.collider.name);
            }
            
	    }

        void OnCollisionEnter2D(Collision2D other) {
            
            if (!has_land) {
                has_land = true; ;
                gravity_change = false;
                Debug.Log("Grounded");
            }
        }

        void OnCollisionExit2D(Collision2D other) {
            //Debug.Log("Deploying");
            has_land = false;
        }

        IEnumerator ChangeGravity() {
            yield return 0;
        }

        IEnumerator Deploy(GroundReference ground, float rotateSpeed) {
            var startingRotation = transform.rotation;
            Quaternion finalRotation = Quaternion.Euler(0, 0, -30) * startingRotation;
            has_deploy = false;
            while (ship_base.rotation != finalRotation)
            {
                ship_base.rotation = Quaternion.Lerp(ship_base.rotation, finalRotation, Time.deltaTime * rotateSpeed);
                yield return 0;
            }
            has_deploy = true;
            startingRotation = finalRotation;
        }
    }
}
