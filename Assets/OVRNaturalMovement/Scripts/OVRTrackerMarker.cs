using UnityEngine;

public class OVRTrackerMarker : MonoBehaviour
{
    [SerializeField] Material lineMaterial;
	float playerEyeHeight;

	void Start () {
        ResetOrientation();
		Invoke("ResetOrientation", 5f);
		playerEyeHeight = OVRManager.profile.eyeHeight;
	}
	
	void Update () {
        UpdateMarker();
	}

    void UpdateMarker()
    {
		var frustum = OVRManager.tracker.frustum;
		float horizontalScale = frustum.fov.x / 100f;
		float verticalScale = frustum.fov.y / 100f;
		transform.localScale = new Vector3(horizontalScale * frustum.farZ, verticalScale * frustum.farZ, frustum.farZ);
    }
	
    public void ResetOrientation()
    {
        OVRManager.display.RecenterPose();
    }   
	    
    void OnRenderObject()
    {    
        lineMaterial.SetPass(0);
        
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(1, 1, 1));

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(-1, 1, 1));

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(1, -1, 1));

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(-1, -1, 1));

        GL.End();
        GL.PopMatrix();
    }
}
