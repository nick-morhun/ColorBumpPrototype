using System;
using UnityEngine;

namespace NickMorhun.ColorBump.Tools
{
	public static class VectorMath
	{
		public static Vector3 ConvertToVector3XZ(this Vector2 vector2)
		{
			return new Vector3(vector2.x, 0, vector2.y);
		}
	}
}