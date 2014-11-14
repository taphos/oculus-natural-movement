using UnityEngine;
using System.Collections;

public class Tutor : MonoBehaviour 
{
	[SerializeField] OVRTrackerMarker cameraMarker;
	[SerializeField] OVRNaturalMovementController motionController;

	[System.NonSerialized] TextMesh textMesh;
	bool spacePressed;

	IEnumerator Start () {
		textMesh = GetComponent<TextMesh>();
		Message("THIS GAME IS MEANT TO BE PLAYED STANDING\nHIT SPACE TO PROCEED");
		yield return StartCoroutine(WaitForSpace());

		Message("CALIBRATING\nLOOK AT POSITION TRACKER AND HIT SPACE");
		yield return StartCoroutine(WaitForSpace());
		
		cameraMarker.ResetOrientation();
		motionController.ResetRotation();
		Message("THE RED BOX IS A TRACKER RANGE\nMAKE A COUPLE OF STEPS INSIDE THE BOX\nDONT STEP OUT OF IT");
		yield return StartCoroutine(WaitForSpace());
		
		Message("JOG IN PLACE TO MOVE FASTER\nSTEPS DETECTED BY HEAD MOTION");
		yield return StartCoroutine(WaitForSpace());
		
		Message("OMG YOU ARE MOVING!");		
		yield return new WaitForSeconds(5);		
		Message("");
	}

	IEnumerator WaitForSpace() {
		while (!spacePressed) yield return new WaitForEndOfFrame();
		spacePressed = false;
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space)) spacePressed = true;
		if (Input.GetKeyDown(KeyCode.R)) {
			cameraMarker.ResetOrientation();
			motionController.ResetRotation();
		}
		if (Input.GetKey(KeyCode.Escape)) Application.Quit();
	} 

    public void Message(string text)
    {
        textMesh.text = text;
    }
}
