using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bombardel.CurveNet.TronClient
{
	public interface ILocatable
	{
		PointF Loc { get; }
	}

	public static class ILocatableExtensions
	{
		public static float Distance(this ILocatable source, ILocatable target)
		{
			return source.Distance(target.Loc);
		}

		public static float Distance(this ILocatable source, PointF loc)
		{
			float dx = source.Loc.X - loc.X;
			float dy = source.Loc.Y - loc.Y;
			float d = (float)Math.Sqrt(dx*dx + dy*dy);
			return d;
		}
	}
}
