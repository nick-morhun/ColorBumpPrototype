using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NickMorhun.ColorBump
{
	public abstract class InputListener : MonoBehaviour
	{
		public virtual void OnDrag(PointerEventData eventData) { }

		public virtual void OnPointerDown(PointerEventData eventData) { }

		public virtual void OnPointerUp(PointerEventData eventData) { }
	}
}