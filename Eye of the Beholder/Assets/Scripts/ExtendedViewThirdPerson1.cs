
using UnityEngine;
using UnityEngine.UI;

public class ExtendedViewThirdPerson1 : ExtendedView
{
    public const int RaycastLayerMask = ~0x24;//0b100100;           // ignore "ignore raycast" and "ui" layers

    public Transform OrbitPoint;
    public SimpleMoveController YawController;
    public SimpleMoveController PitchController;
    public float MinimumPitch = -90;
    public float MaximumPitch = 90;
    public float ZoomDistance = 2;
    private Camera _usedCamera;
    
    protected override void Start()
    {
        base.Start();
        _usedCamera = GetComponent<Camera>();
    }


    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && ZoomDistance < 6) // forward
        {
            ZoomDistance++;
            OnPreCull();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && ZoomDistance>0) // backwards
        {
            ZoomDistance--;
            OnPreCull();
        }
    }

    private void OnPreCull()
    {
        var localRotation = OrbitPoint.localRotation;

        transform.position = OrbitPoint.position - OrbitPoint.forward * ZoomDistance;
        transform.rotation = OrbitPoint.rotation;

        UpdateCameraWithoutExtendedView(_usedCamera);
        var worldUp = Vector3.up;
        Rotate(OrbitPoint, up: worldUp);

        transform.position = OrbitPoint.position - OrbitPoint.forward * ZoomDistance;
        transform.rotation = OrbitPoint.rotation;

        UpdateCameraWithExtendedView(_usedCamera);
        

        StartCoroutine(ResetCameraLocal(localRotation, OrbitPoint));
    }

    public override void AimAtWorldPosition(Vector3 worldPostion)
    {
        var direction = worldPostion - OrbitPoint.position;
        var rotation = Quaternion.LookRotation(direction);

        InitAimAtGazeOffset(Mathf.DeltaAngle(rotation.eulerAngles.y, _usedCamera.transform.rotation.eulerAngles.y),
            Mathf.DeltaAngle(rotation.eulerAngles.x, _usedCamera.transform.rotation.eulerAngles.x));

        if (YawController != null
            && PitchController != null)
        {
            YawController.SetRotation(rotation);
            PitchController.SetRotation(rotation);
        }
    }
}