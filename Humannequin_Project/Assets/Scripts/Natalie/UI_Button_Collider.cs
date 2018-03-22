// UI_BUTTON_COLLIDER
// https://unity3d.college/2017/06/17/steamvr-laser-pointer-menus/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UI_Button_Collider : MonoBehaviour
{
	private BoxCollider boxCollider;
	private RectTransform rectTransform;

	private void OnEnable()
	{
		ValidateCollider();
	}

	private void OnValidate()
	{
		ValidateCollider();
	}

	private void ValidateCollider()
	{
		rectTransform = GetComponent<RectTransform>();

		boxCollider = GetComponent<BoxCollider>();
		if (boxCollider == null)
		{
			boxCollider = gameObject.AddComponent<BoxCollider>();
		}

		boxCollider.size = rectTransform.sizeDelta;
	}
}