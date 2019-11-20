using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator animator;
    private Button button;

    private void Awake()
    {
        if (this.button == null || this.animator == null)
        {
            this.animator = this.GetComponent<Animator>();
            this.button = this.GetComponent<Button>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("hovering", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("hovering", false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetBool("clicking", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        animator.SetBool("clicking", false);

        //this.OnClick();
        //button.onClick.Invoke();
    }

    public virtual void OnClick()
    {

    }

    public void RegisterOnClickEvent(UnityAction action)
    {
        if (button == null)
        {
            Awake();
        }
        this.button.onClick.AddListener(action);
    }

    public void ClearEvents()
    {
        this.button.onClick.RemoveAllListeners();
    }
}
