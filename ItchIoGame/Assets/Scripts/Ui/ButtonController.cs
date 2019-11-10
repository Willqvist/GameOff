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

    [SerializeField] private string text;
    [SerializeField] private string name;
    [SerializeField] private Animator animator;
    [SerializeField] private IClickHandler onClick;
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    private void OnValidate()
    {
        if(this.textMesh == null)
            this.textMesh = this.GetComponentInChildren<TextMeshProUGUI>();
        this.textMesh.text = text;
        this.animator = this.GetComponent<Animator>();
    }
    void Start()
    {
        this.textMesh = this.GetComponentInChildren<TextMeshProUGUI>();
        this.textMesh.text = text;
    }

    // Update is called once per frame
    void Update()
    {

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
        if(this.onClick == null)
        {
            Debug.Log("On click is null in ButtonController::OnPointerUp");
            return;
        }

        animator.SetBool("clicking", false);
        onClick.OnClick(this);
    }

    public string getName() {
        return name;
    }
}
