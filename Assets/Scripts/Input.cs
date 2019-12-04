using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public class Input : MonoBehaviour, IDragHandler
	{
		[SerializeField]
		private DragInputListener[] _dragListeners;

		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			for (int i = 0; i < _dragListeners.Length; i++)
			{
				_dragListeners[i]?.OnDrag(eventData);
			}
		}
	}
}