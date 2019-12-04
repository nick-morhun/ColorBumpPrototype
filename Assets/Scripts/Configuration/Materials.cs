using JetBrains.Annotations;
using UnityEngine;

namespace NickMorhun.ColorBump.Configuration
{
	[CreateAssetMenu(fileName ="Materials", menuName ="ColorBump/Materials")]
	public sealed class Materials : ScriptableObject
	{
		[SerializeField, CanBeNull]
		private Material _hazardMaterial;

		[SerializeField, CanBeNull]
		private Material _neutralMaterial;

		public Material HazardMaterial => _hazardMaterial;

		public Material NeutralMaterial => _neutralMaterial;
	}
}