using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour
{
    GameObject cueBall;
    Vector3 cueBallPos;
    Rigidbody cueRB;
    Rigidbody playerRigidbody;
    Vector3 currentDirection;
    bool posCue = true;
    bool selectBall = false;
    Vector3 selectedBall;
    GameControl gs;



    // Use this for initialization
    void Start()
    {

        cueBall = GameObject.Find("cue");
        cueBallPos = cueBall.transform.position;
        cueRB = cueBall.GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider col)
    {
        //Transform trans = col.gameObject.transform;   
        //col.gameObject.transform.position = Vector3.Lerp(trans.position, new Vector3(trans.position.x, trans.position.y - 1, trans.position.z), 1f);
        if (col.gameObject.transform.name == "cue")
        {
            col.gameObject.transform.position = cueBallPos + new Vector3(0f,.1f,0f);
            cueRB.velocity = Vector3.zero;
            cueRB.angularVelocity = Vector3.zero;
            posCue = true;
        }
        else
        {
            Destroy(col.gameObject);
        }
    }      

	void shotPosition(Transform t) {
		Ray shotRay = new Ray (t.position, t.position - cueRB.transform.position);
	}
    
	
	bool newDirection()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		int floorMask = LayerMask.GetMask("TableFloor");
		if (Physics.Raycast(camRay, out floorHit))
		{
			Vector3 playerToMouse = floorHit.point - cueRB.transform.position;
			playerToMouse.y = 0f;
			currentDirection = playerToMouse;
			return true;
		}
		return false;
	}

    void Update()
    {
        if (posCue)
        {
            newDirection();
            Vector3 temp = cueRB.transform.position + currentDirection;

            //Vector3 newCueBallPos = new Vector3(Mathf.Clamp(temp.x, 1.17f, 1.57f), 0f, Mathf.Clamp(temp.z, 0.49f, -.48f));
            //Debug.Log("newCueBallPos " + newCueBallPos);
            cueRB.transform.position = Vector3.Lerp(cueRB.transform.position, cueRB.transform.position + currentDirection, Time.deltaTime * 2);
            //cueRB.transform.position = Vector3.Lerp(cueRB.transform.position, newCueBallPos, Time.deltaTime * 2);
        }
		if (Input.GetKeyDown("space"))
		{
			posCue = false;
            selectBall = true;
		}
        if (selectBall)
        {
            Ray ballRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ballRay, out hit))
            {
                if (hit.collider.tag == "ball" && Input.GetMouseButton(1))
                {
                    selectedBall = hit.collider.transform.position;
                    Vector3 dist = cueRB.transform.position - selectedBall;
                    Ray cueStickRay = new Ray (selectedBall, selectedBall - cueRB.transform.position);

                    //CueStickControl.getInstance().lineUpShot(cueStickRay.GetPoint();

                }
            }

        }
    }

}