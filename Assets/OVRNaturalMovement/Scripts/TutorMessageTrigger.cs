using UnityEngine;
using System.Collections;

public class TutorMessageTrigger : MonoBehaviour {

	[SerializeField] string text = "CROUCH";

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			StartCoroutine(ShowMessage());
		}
	}

	IEnumerator ShowMessage()
	{
		var tutor = FindObjectOfType(typeof(Tutor)) as Tutor;
		tutor.Message(text);
		yield return new WaitForSeconds(3);
		tutor.Message("");
	}
}
