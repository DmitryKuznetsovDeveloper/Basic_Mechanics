using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class BasePopupView : MonoBehaviour
{
    [FoldoutGroup("Main Settings")] [SerializeField]
    private CanvasGroup mainCanvasGroup;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private Image backgroundBlack;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private CanvasGroup contentCanvasGroup;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private RectTransform contentPanel;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private TMP_Text titleLabel;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private TMP_Text descriptionLabel;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private Image imageCharacter;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private RectTransform buttonOkTransform;

    [FoldoutGroup("Animated objects")] [SerializeField]
    private CanvasGroup buttonOkCanvasGroup;


    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 startContentPosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 endContentPosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 startCharacterImagePosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 endCharacterImagePosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 startTitlePosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 endTitlePosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 startDescriptionPosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 endDescriptionPosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 startButtonOkPosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Vector3 endButtonOkPosition;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private float durationShowAlpha;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private float durationContent;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private float durationMoveTextAndImage;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Ease easeShowContent;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Ease easeHideContent;

    [FoldoutGroup("Settings Anim")] [SerializeField]
    private Ease easeShowTextAndImage;

    private Sequence _sequenceShowPopup;
    private Sequence _sequenceHide;
    private Sequence _sequenceShowDialogue;
    private Sequence _sequenceHideDialogue;

    public void ShowBasePopup(string title, string description, Image spriteCharacter)
    {
        titleLabel.text = title;
        descriptionLabel.text = description;
        imageCharacter = spriteCharacter;
        SwitchEnableMainCanvasGroup(true, 1f);
        _sequenceShowPopup.OnComplete((() => _sequenceShowDialogue.Restart())).Restart();
    }

    public void DialogChangeBasePopup(string title, string description, Image spriteCharacter) =>
        _sequenceHideDialogue.OnComplete((() =>
        {
            titleLabel.text = title;
            descriptionLabel.text = description;
            imageCharacter = spriteCharacter;
            _sequenceShowDialogue.Restart();
        })).Restart();

    public void HideBasePopup() => _sequenceHide.OnComplete((() => SwitchEnableMainCanvasGroup(false, 0f))).Restart();

    private void Awake()
    {
        SwitchEnableMainCanvasGroup(false, 0f);
        _sequenceHide = DOTween.Sequence();
        _sequenceHide.Join(contentPanel.DOLocalMove(startContentPosition, durationContent).SetEase(easeHideContent)
            .From(endContentPosition));
        _sequenceHide.Join(buttonOkCanvasGroup.DOFade(0f, durationContent).From(1f));
        _sequenceHide.Join(descriptionLabel.DOFade(0f, durationContent).From(1f));
        _sequenceHide.Join(titleLabel.DOFade(0f, durationContent).From(1f));
        _sequenceHide.Join(imageCharacter.DOFade(0f, durationContent).From(1f));
        _sequenceHide.Join(contentCanvasGroup.DOFade(0f, durationContent).From(1f));
        _sequenceHide.Join(backgroundBlack.DOFade(0f, durationContent).From(0.8f));
        _sequenceHide.SetAutoKill(false);
        _sequenceHide.Pause();

        _sequenceHideDialogue = DOTween.Sequence();
        _sequenceHideDialogue.Join(imageCharacter.rectTransform
            .DOLocalMove(startCharacterImagePosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(endCharacterImagePosition));
        _sequenceHideDialogue.Join(imageCharacter.DOFade(0f, durationShowAlpha).From(1f));
        _sequenceHideDialogue.Join(titleLabel.transform
            .DOLocalMove(startTitlePosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(endTitlePosition));
        _sequenceHideDialogue.Join(titleLabel.DOFade(0f, durationShowAlpha).From(1f));
        _sequenceHideDialogue.Join(descriptionLabel.transform
            .DOLocalMove(startDescriptionPosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(endDescriptionPosition));
        _sequenceHideDialogue.Join(descriptionLabel.DOFade(0f, durationShowAlpha).From(1f));
        _sequenceHideDialogue.Join(buttonOkTransform.transform
            .DOLocalMove(startButtonOkPosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(endButtonOkPosition));
        _sequenceHideDialogue.Join(buttonOkCanvasGroup.DOFade(0f, durationShowAlpha).From(1f));
        _sequenceHideDialogue.SetAutoKill(false);
        _sequenceHideDialogue.Pause();

        _sequenceShowPopup = DOTween.Sequence();
        _sequenceShowPopup.Append(backgroundBlack.DOFade(0.8f, durationShowAlpha).From(0f));
        _sequenceShowPopup.Join(contentPanel.DOLocalMove(endContentPosition, durationContent).SetEase(easeShowContent)
            .From(startContentPosition));
        _sequenceShowPopup.Join(contentCanvasGroup.DOFade(1f, durationContent).From(0f));
        _sequenceShowPopup.SetAutoKill(false);
        _sequenceShowPopup.Pause();

        _sequenceShowDialogue = DOTween.Sequence();
        _sequenceShowDialogue.Append(imageCharacter.DOFade(1f, durationMoveTextAndImage).From(0f));
        _sequenceShowDialogue.Join(imageCharacter.rectTransform
            .DOLocalMove(endCharacterImagePosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(startCharacterImagePosition));
        _sequenceShowDialogue.Append(titleLabel.DOFade(1f, durationMoveTextAndImage).From(0f));
        _sequenceShowDialogue.Join(titleLabel.transform.DOLocalMove(endTitlePosition, durationMoveTextAndImage)
            .SetEase(easeShowTextAndImage).From(startTitlePosition));
        _sequenceShowDialogue.Append(descriptionLabel.DOFade(1f, durationMoveTextAndImage).From(0f));
        _sequenceShowDialogue.Join(descriptionLabel.transform
            .DOLocalMove(endDescriptionPosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(startDescriptionPosition));
        _sequenceShowDialogue.Append(buttonOkCanvasGroup.DOFade(1f, durationMoveTextAndImage).From(0f));
        _sequenceShowDialogue.Join(buttonOkTransform.transform
            .DOLocalMove(endButtonOkPosition, durationMoveTextAndImage).SetEase(easeShowTextAndImage)
            .From(startButtonOkPosition));
        _sequenceShowDialogue.SetAutoKill(false);
        _sequenceShowDialogue.Pause();
    }

    private void SwitchEnableMainCanvasGroup(bool value, float alpha = 1f)
    {
        mainCanvasGroup.interactable = value;
        mainCanvasGroup.blocksRaycasts = value;
        mainCanvasGroup.alpha = alpha;
    }

    private void OnDisable()
    {
        _sequenceShowPopup.Kill();
        _sequenceHide.Kill();
        _sequenceShowDialogue.Kill();
        _sequenceHideDialogue.Kill();
    }
}