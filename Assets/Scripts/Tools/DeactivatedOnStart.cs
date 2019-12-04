using UnityEngine;

namespace NickMorhun.ColorBump.Tools
{
	[DisallowMultipleComponent]
	public sealed class DeactivatedOnStart : MonoBehaviour
	{
		private void Start()
		{
			gameObject.SetActive(false);
		}
	}
}