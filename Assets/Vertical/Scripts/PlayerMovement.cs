using UnityEngine;
using System.Collections;

namespace Vertical
{
    public class PlayerMovement : MonoBehaviour {
        public PlayerMovement instance = null;
        public float m_Speed = 10;

        private float vAxis;
        private float hAxis;
		public bool OnAir;
		public bool lSideGrounded;
		public bool rSideGrounded;

		public float rotateSpeed;
		public float rotateOffset;
		private Transform ship_Base;
		private Quaternion startingRotation;
		private bool rotating;

		private Transform ship_Weapon;

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
			ship_Base = transform.FindChild("Base");
			ship_Weapon = transform.FindChild("Weapon");
			startingRotation = ship_Base.rotation;
	    }
	
	    // Update is called once per frame
	    void Update () {
            
            hAxis = Input.GetAxis("Horizontal");
            if (hAxis != 0) {
				float xMove = hAxis * Time.deltaTime * m_Speed;
	            transform.Translate(xMove, 0, 0);
	            float leftLim = leftSide.position.x + leftOffset;
				float rightLim = rightSide.position.x -rightOffset;
	            Debug.Log(leftLim+" : "+rightLim);
				transform.position = new Vector2(Mathf.Clamp(transform.position.x, leftLim, rightLim), transform.position.y);
            }
			lSideGrounded = transform.position.x - 0.01f <= leftSide.position.x + leftOffset;
			rSideGrounded = transform.position.x + 0.01f >= rightSide.position.x - rightOffset;
			OnAir = (transform.position.x < leftSide.position.x + leftOffset) || (transform.position.x < rightSide.position.x - rightOffset);
			if (lSideGrounded) {
				Debug.Log ("Ship Stay on left side");
			}
			if (rSideGrounded) {
				Debug.Log ("Ship Stay on right side");
			}
            
	    }

		IEnumerator rotateTriangle(float rotationAmount)
		{
			Debug.Log ("Rotating");
			Quaternion finalRotation = Quaternion.Euler(0, 0, rotationAmount) * startingRotation;
			rotating = true;
			while (ship_Base.rotation != finalRotation)
			{
				ship_Base.rotation = Quaternion.Lerp(ship_Base.rotation, finalRotation, Time.deltaTime * rotateSpeed);
				yield return 0;
			}
			rotating = false;
			startingRotation = finalRotation;
			
		}
    }
}
