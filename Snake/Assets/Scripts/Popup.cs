using UnityEngine;
using TMPro;
using DG.Tweening;

public class Popup : MonoBehaviour
{
    [Header("Where To Move Popup")]
    [SerializeField] private Vector3 vectorChange = new Vector2(0f, 0.5f);

    [Header("What To Add Before Number")]
    [SerializeField] private string prefixForNumber = "+";

    [Header("Flashig Color")]
    [SerializeField] private Color colorToChangeTo = Color.white;
    [SerializeField] private float howFastToFlashColor = 0.2f;

    [Header("Tween In")]
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float tweenDurationIn = 0.5f;
    [SerializeField] private Ease easeIn = Ease.InOutQuad;

    [Header("Tween Out")]
    [SerializeField] private float tweenDurationOut = 1f;
    [SerializeField] private Ease easeOut = Ease.InOutQuad;

    [Header("REFERERNCES")]
    [SerializeField] private TextMeshPro textMesh;

    private Vector3 basePosition;

    [SerializeField] private RectTransform rect;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        rect = transform.GetComponent<RectTransform>();

        rect.DOScale(0, 0);
        basePosition = rect.position;
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText($"{prefixForNumber}{damageAmount}");

        rect.DOScale(maxScale, tweenDurationIn)
            .SetEase(easeIn)
            .OnComplete(() => TweenOut());

        textMesh.DOColor(colorToChangeTo, howFastToFlashColor)
            .SetLoops(-1);

        rect.DOMove(rect.position + vectorChange, tweenDurationIn)
            .SetEase(easeIn);
    }

    private void TweenOut()
    {
        rect.DOScale(0, tweenDurationOut)
            .SetDelay(1f)
            .SetEase(easeOut)
            .OnComplete(() => Destroy(gameObject));

        //rect.DOMove(basePosition, tweenDurationOut)
        //    .SetEase(easeOut);

        textMesh.DOFade(0, tweenDurationOut - 0.5f)
            .SetEase(easeOut);
    }
}
