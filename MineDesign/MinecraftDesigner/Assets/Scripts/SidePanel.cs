﻿using UnityEngine;
using UnityEngine.UI; // UI
using UnityEngine.EventSystems; // UI
using System.Collections;

public class SidePanel : MonoBehaviour, IPointerDownHandler, IDragHandler {
    private Vector2 pointerOffset;
    private RectTransform canvasRectTransform;
    private RectTransform panelRectTransform;

    void Awake () {
        Canvas canvas = GetComponentInParent <Canvas> ();

        if (canvas != null) {
            canvasRectTransform = canvas.transform as RectTransform;
            panelRectTransform = transform as RectTransform;
        }
    }

    public void OnPointerDown (PointerEventData data) {
        panelRectTransform.SetAsLastSibling ();
        RectTransformUtility.ScreenPointToLocalPointInRectangle (panelRectTransform,
            data.position,
            data.pressEventCamera,
            out pointerOffset);
    }

    public void OnDrag (PointerEventData data) {
        if (panelRectTransform == null) {
            return;
        }

        Vector2 localPointerPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle (canvasRectTransform,
            data.position,
            data.pressEventCamera,
            out localPointerPosition)) {
            panelRectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }
}
