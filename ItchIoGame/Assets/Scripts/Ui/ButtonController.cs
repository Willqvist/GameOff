using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] protected string identifier;
    [SerializeField] protected Animator animator;
    [SerializeField] protected IClickHandler onClick;

    private void Start()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void SetIdentifier(string id)
    {
        this.identifier = id;
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

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if(this.onClick == null)
        {
            Debug.Log("On click is null in ButtonController::OnPointerUp");
            return;
        }

        animator.SetBool("clicking", false);
        onClick.OnClick(this);
    }

    public string getName() {
        return identifier;
    }
}
