using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public static CameraScript instance = null;

    public Rect cameraRect;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(this);
	}

    void Start() { 
        
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint (Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, Camera.main.pixelHeight));

        Rect cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
