using UnityEngine;

namespace NickMorhun.ColorBump.Configuration
{
	[CreateAssetMenu(fileName ="Tags", menuName ="ColorBump/Tags")]
	public sealed class Tags : ScriptableObject
	{
		[SerializeField]
		private string _hazardTag = "Hazard";

		[SerializeField]
		private string _neutralTag = string.Empty;

		public string HazardTag  => _hazardTag;

		public string NeutralTag => _neutralTag;
	}
}