using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ButtonExitButtonView : MonoBehaviour
    {
        [field: SerializeField] public Button Button{ get; private set;}
        [SerializeField] private RectTransform scaleRectTransform;
        [SerializeField] private float scaleFactor;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private AudioSource audioClick;
        
        public void ButtonAnimOnClick( Action action)
        {
            audioClick.Play();
            scaleRectTransform
                .DOScale(scaleFactor, duration)
                .SetLoops(2, LoopType.Yoyo).SetEase(ease).OnComplete(() => { action?.Invoke(); });
        }
    }
}