using JetBrains.Annotations;
using System.Collections.Generic;

namespace NickMorhun.ColorBump
{
	public interface ILevelDataSource
	{
		[NotNull]
		IEnumerable<Obstacle> GetObstacles();
	}
}