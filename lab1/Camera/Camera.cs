using System.Numerics;

class Camera
{
    public Transform Transform;
    private Vector3 _cameraPosition = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 _cameraFront = new Vector3(0.0f, 0.0f, -1.0f);
    private Vector3 _cameraUp = Vector3.UnitY;
    private Vector3 _cameraDirection = Vector3.Zero;

    public Camera()
    {
        Transform = new Transform();
        Transform.Position = _cameraPosition;
    }

    public Vector3 CameraPosition
    {
        get => _cameraPosition;
        set
        {
            _cameraPosition = value;
            Transform.Position = _cameraPosition;
        }
    }
}