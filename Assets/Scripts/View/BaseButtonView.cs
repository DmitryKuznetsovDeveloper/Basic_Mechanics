using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public sealed class BaseButtonView : MonoBehaviour
    {
        [field: SerializeField] public Button Button{ get; private set;}
        [SerializeField] private TMP_Text countLabel;
        [SerializeField] private Image fillImage;
        [SerializeField] private Image imageCharacter;
        [SerializeField] private Image blockImage;
        [SerializeField] private RectTransform scaleRectTransform;
        [SerializeField] private float scaleFactor;
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        [SerializeField] private AudioSource audioClick;

        public void SetLabel(string value) => countLabel.text = value;
        public void SetImageCharacter(Texture2D texture2D)
        {
            var mySprite = Sprite.Create(texture2D,
                new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            imageCharacter.sprite = mySprite;
        
        }

        public void SwitchEnableButton(bool value)
        {
            Button.interactable = value;
            blockImage.enabled = !value;
        }
        
        public void ButtonAnimOnClick(float durationFill, Action action)
        {
            audioClick.Play();
            Button.interactable = false;
            scaleRectTransform
                .DOScale(scaleFactor, duration)
                .SetLoops(2, LoopType.Yoyo).SetEase(ease).OnComplete(() =>
                {
                    fillImage.DOFillAmount(1f, durationFill).OnComplete(() =>
                    {
                        action?.Invoke();
                        fillImage.fillAmount = 0f;
                        Button.interactable = true;
                    });
                });
        }
    }
}