using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class BaseHudIndicatorView : MonoBehaviour
    {
        [SerializeField] private TMP_Text countLabel;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image scaleImage;
        [SerializeField] private float scaleFactor;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;

        public void SetLabel(string value)
        {
            countLabel.text = value;
            scaleImage.rectTransform.DOScale(scaleFactor, duration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        }

        public void SetTimeFillImage(float time)
        {
              fillImage.fillAmount += 1 / time * Time.deltaTime;
                if(fillImage.fillAmount >= 0.99f)
                    fillImage.fillAmount = 0f;
        }

        public void SetFillImage(float currentFill, float MaxFill) => fillImage.DOFillAmount(currentFill / MaxFill,duration);
    }
}