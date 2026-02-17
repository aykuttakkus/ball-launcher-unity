using UnityEngine;
using Unity.Cinemachine;

[ExecuteAlways]
public class FitOrthoToBounds : MonoBehaviour
{
    [Header("References")]
    public BoxCollider2D playableBounds;
    public CinemachineCamera cmCamera;   // Cinemachine 3
    public Camera outputCamera;          // Main Camera

    [Header("Tuning")]
    public float marginWorldUnits = 0.5f;

    void Reset()
    {
        cmCamera = GetComponent<CinemachineCamera>();
        outputCamera = Camera.main;
    }

    void LateUpdate()
    {
        Apply();
    }

    public void Apply()
    {
        if (!playableBounds || !cmCamera) return;

        float aspect = outputCamera ? outputCamera.aspect : (float)Screen.width / Screen.height;

        Bounds b = playableBounds.bounds;

        float needSizeY = b.size.y * 0.5f;
        float needSizeX = (b.size.x * 0.5f) / aspect;

        float ortho = Mathf.Max(needSizeY, needSizeX) + marginWorldUnits;

        cmCamera.Lens.OrthographicSize = ortho;

        var pos = cmCamera.transform.position;
        cmCamera.transform.position = new Vector3(b.center.x, b.center.y, pos.z);
    }
}