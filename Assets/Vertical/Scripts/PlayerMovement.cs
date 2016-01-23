using UnityEngine;
using System.Collections;

namespace Vertical
{
    public class PlayerMovement : MonoBehaviour {
        public PlayerMovement instance = null;
        public float m_Speed = 10;

        private float vAxis;
        private float hAxis;

        [HideInInspector]
        public Transform leftSide;
        public float leftOffset = 0.5f;
        [HideInInspector]
        public Transform rightSide;
        public float rightOffset = 0.5f;

        void Awake() {
            if (instance == null)
                instance = this;
            else if (instance != null)
                Destroy(this);
            
        }

	    // Use this for initialization
	    void Start () {
            leftSide = GameObject.Find("LeftSideEdge").transform;
            rightSide = GameObject.Find("RightSideEdge").transform;
	    }
	
	    // Update is called once per frame
	    void Update () {
            float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * m_Speed;
            transform.Translate(xMove, 0, 0);
            float leftLim = leftSide.position.x + leftOffset;
            float rightLim = rightOffset - rightSide.position.x;
            Debug.Log(leftLim+" : "+rightLim);
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, -3f, 3f), transform.position.y);
            /*
            vAxis = Input.GetAxis("Horizontal");
            if (vAxis != 0) {
                var xMove = Input.GetAxis("Horizontal") * Time.deltaTime * m_Speed;
                
                var leftLim = leftSide.position.x + leftOffset;
                var rightLim = rightSide.position.x - rightOffset;
                transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftLim, rightLim), transform.position.y);
            }
            */
	    }
    }
}
