using UnityEngine;

namespace NickMorhun.ColorBump
{
	public readonly struct ObstacleData
	{
		public readonly bool IsHazard;
		public readonly Vector3 WorldPostion;
		public readonly int Type;

		public ObstacleData(bool isHazard, Vector3 worldPostion, int type)
		{
			IsHazard = isHazard;
			WorldPostion = worldPostion;
			Type = type;
		}
	}
}