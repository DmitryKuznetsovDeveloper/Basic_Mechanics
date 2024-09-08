using DG.Tweening;
using UnityEngine;
using Zenject;

public sealed class CameraFollower : ILateTickable
{
    private readonly Camera _camera;
    private readonly Transform _characterTransform;
    private readonly Vector3 _offset;
    private readonly float _smoothTime = 0.3f;
    private Vector3 _currentVelocityMove;

    public CameraFollower(Camera camera, Transform characterTransform, Vector3 offset)
    {
        _camera = camera;
        _characterTransform = characterTransform;
        _offset = offset;
    }

    public void LateTick() => 
        Follow();

    private void Follow()
    {
        var charPosition = _characterTransform.position;
        var directionTarget = charPosition + _offset;
        _camera.transform.DOLookAt(charPosition,_smoothTime);
        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, directionTarget, ref _currentVelocityMove,
            _smoothTime * Time.deltaTime);
    }
}