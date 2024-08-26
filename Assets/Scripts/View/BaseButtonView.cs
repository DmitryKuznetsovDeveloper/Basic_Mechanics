using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countLabel;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image imageCharacter;
        [SerializeField] private RectTransform scaleRectTransform;
        [SerializeField] private float scaleFactor;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;


        public void SetLabel(string value) => countLabel.text = value;
        public void SetImageCharacter(Image value) => imageCharacter = value;
        public void SetFillImage(float value) => fillImage.DOFillAmount(value, duration);

        public void ButtonAnimOnClick(Action action) => scaleRectTransform.DOScale(scaleFactor, duration)
            .SetLoops(2, LoopType.Yoyo).SetEase(ease).OnComplete(() => action?.Invoke());
    }
}