using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopOptionUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Button button;
    [SerializeField] TMP_Text text;
    [SerializeField] Image selectedOutline;

    [SerializeField] Color textHighlightColor;
    private Color textNormalColor;

    private void Start()
    {
        textNormalColor = this.text.color;
    }

    public void Setup(string text, Action onClickCallback)
    {
        this.text.text = text;
        button.onClick.AddListener(() => onClickCallback?.Invoke());
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        selectedOutline.enabled = true;
        text.color = textHighlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selectedOutline.enabled = false;
        text.color = textNormalColor;
    }
}
