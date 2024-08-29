using DG.Tweening;
using UnityEngine;

public class FollowThePoints : MonoBehaviour
{
    [SerializeField] private GameObject moveObject;
    [SerializeField] private Transform[] pointsFollow;
    [SerializeField] private float moveDuration;
    [SerializeField] private float rotationDuration;
    private bool _isFlagGo = true;
    private int _index;

    void Update()
    {
        if (_isFlagGo)
        {
            if (_index >= pointsFollow.Length)
                _index = 0;
            if (moveObject.transform.localPosition != pointsFollow[_index].localPosition)
            {
                _isFlagGo = false;
                moveObject.transform.DOLookAt(pointsFollow[_index].localPosition, rotationDuration);
                moveObject.transform.DOLocalMove(pointsFollow[_index].localPosition, moveDuration)
                    .OnComplete((() =>
                    {
                        _index++;
                        _isFlagGo = true;
                    }));
            }
        }
    }
}