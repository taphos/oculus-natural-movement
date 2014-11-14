using UnityEngine;

public class OVRTrackerMarker : MonoBehaviour
{
    Ovr.TrackingState trackingState;
    Ovr.HmdDesc hmdDesc;
    OVRCameraRig rig;

    Material lineMaterial;
	float playerEyeHeight;

	void Start () {
        rig = transform.parent.GetComponentInChildren<OVRCameraRig>();
        ResetOrientation();
		Invoke("ResetOrientation", 5f);
        InitWireframe();
		playerEyeHeight = OVRNaturalMovementController.GetPlayerEyeHeight();
	}
	
	void Update () {
        UpdateMarker();
	}

    void UpdateMarker()
    {
		transform.localPosition = trackingState.CameraPose.Position.ToVector3() + Vector3.up * playerEyeHeight;
        transform.localRotation = trackingState.CameraPose.Orientation.ToQuaternion();
        
        float horizontalScale = Mathf.Tan(hmdDesc.CameraFrustumHFovInRadians / 2f);
        float verticalScale = Mathf.Tan(hmdDesc.CameraFrustumVFovInRadians / 2f);        
        transform.localScale = new Vector3 (horizontalScale * hmdDesc.CameraFrustumFarZInMeters, verticalScale * hmdDesc.CameraFrustumFarZInMeters, hmdDesc.CameraFrustumFarZInMeters);
    }
	
    public void ResetOrientation()
    {
        OVRManager.capiHmd.RecenterPose();
        trackingState = OVRManager.capiHmd.GetTrackingState();
        hmdDesc = OVRManager.capiHmd.GetDesc();
    }   

    void InitWireframe()
    {     
        lineMaterial = new Material("Shader \"Lines/Colored Blended\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha BindChannels { Bind \"Color\",color } ZWrite Off Cull Front Fog { Mode Off } } } }");
        
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
    }
    
    void OnRenderObject()
    {    
        if (Camera.current == rig.camera) return;

        lineMaterial.SetPass(0);
        
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(1, 1, -1));

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(-1, 1, -1));

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(1, -1, -1));

        GL.Vertex(Vector3.zero);
        GL.Vertex(new Vector3(-1, -1, -1));

        GL.End();
        GL.PopMatrix();
    }
}
