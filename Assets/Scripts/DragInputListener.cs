using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NickMorhun.ColorBump
{
	public abstract class DragInputListener : MonoBehaviour
	{
		public abstract void OnDrag(PointerEventData eventData);
	}
}