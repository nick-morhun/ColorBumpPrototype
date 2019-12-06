using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace NickMorhun.ColorBump
{
	[DisallowMultipleComponent]
	public class Input : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
	{
		[SerializeField]
		[FormerlySerializedAs("_dragListeners")]
		private InputListener[] _listeners;

		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			for (int i = 0; i < _listeners.Length; i++)
			{
				_listeners[i]?.OnDrag(eventData);
			}
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			for (int i = 0; i < _listeners.Length; i++)
			{
				_listeners[i]?.OnPointerDown(eventData);
			}
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			for (int i = 0; i < _listeners.Length; i++)
			{
				_listeners[i]?.OnPointerUp(eventData);
			}
		}
	}
}