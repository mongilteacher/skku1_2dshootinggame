using System;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

                                             // 인터페이스
public class UI_TouchBounce : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // 추가 기능
    // - 피봇 설정 기능
    // - 버튼이 있을 경우 버튼이 활성화 됬을때만
    // - 이징
    
    
    // 마우스를 터치하면 크기를 변경(키우거나 늘리거나)
    public float StartScale = 1f;
    public float EndScale = 1.2f;
    public float Duration = 0.1f;
    
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        _rectTransform.localScale = new Vector3(StartScale, StartScale, 1f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _rectTransform.DOScale(new Vector3(EndScale, EndScale, 1f), Duration).SetUpdate(true).;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _rectTransform.DOScale(new Vector3(StartScale, StartScale, 1f), Duration).SetUpdate(true);
    }
}
