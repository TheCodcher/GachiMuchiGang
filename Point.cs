using System;
using System.Drawing;

namespace Dungeon_Master
{
	public struct Point
	{
		public int X;
		public int Y;

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Checks is current point on board or out of range.
		/// </summary>
		/// <param name="size">Board size to comapre</param>
		public bool IsOutOf(int size)
		{
			return X >= size || Y >= size || X < 0 || Y < 0;
		}
        public bool IsOutOf(int Width, int Height)
        {
            return X >= Width || Y >= Height || X < 0 || Y < 0;
        }
        public override string ToString()
		{
			return string.Format("[{0},{1}]", X, Y);
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			if (!(obj is Point)) return false;

			Point that = (Point)obj;

			return that.X == this.X && that.Y == this.Y;
		}

		public override int GetHashCode()
		{
			return (X.GetHashCode() ^ Y.GetHashCode());
		}
        public static Point operator +(Point A, Point B) => new Point(A.X + B.X, A.Y + B.Y);
        public static Point operator +(Point A, int b) => new Point(A.X + b, A.Y + b);
        public static Point operator *(Point A, Point B) => new Point(A.X * B.X, A.Y * B.Y);
        public static Point operator *(Point A, int b) => new Point(A.X * b, A.Y * b);
        public static Point operator /(Point A, Point B) => new Point(A.X / B.X, A.Y / B.Y);
        public static Point operator /(Point A, int b) => new Point(A.X / b, A.Y / b);
        public static Point operator *(Point A, double b) => new Point((int)Math.Round(A.X * b), (int)Math.Round(A.Y * b));
        public static Point operator -(Point A, Point B) => new Point(A.X - B.X, A.Y - B.Y);
        public static Point operator -(Point A, int b) => new Point(A.X - b, A.Y - b);
        public static Point operator ++(Point A) => new Point(A.X + 1, A.Y + 1);
        public static Point operator --(Point A) => new Point(A.X - 1, A.Y - 1);
        public static bool operator ==(Point A, Point B) => (A.X == B.X) && (A.Y == B.Y) ? true : false;
        public static bool operator ==(Point A, int b) => (A.X == b) && (A.Y == b) ? true : false;
        public static bool operator !=(Point A, Point B) => (A.X != B.X) || (A.Y != B.Y) ? true : false;
        public static bool operator !=(Point A, int b) => (A.X != b) || (A.Y != b) ? true : false;
        //.///.////

        public static bool operator <=(Point A, int b)
        {
            return (A.X <= b) && (A.Y <= b) ? true : false;
        }
        public static bool operator >=(Point A, int b)
        {
            return (A.X >= b) || (A.Y >= b) ? true : false;
        }
        public static bool operator <=(Point A, Point B)
        {
            return (A.X <= B.X) && (A.Y <= B.Y) ? true : false;
        }
        public static bool operator >=(Point A, Point B)
        {
            return (A.X >= B.X) && (A.Y >= B.Y) ? true : false;
        }

        //.///.////

        public static bool operator <(Point A, int b)
        {
            return (A.X < b) && (A.Y < b) ? true : false;
        }
        public static bool operator >(Point A, int b)
        {
            return (A.X > b) || (A.Y > b) ? true : false;
        }
        public static bool operator <(Point A, Point B)
        {
            return (A.X < B.X) && (A.Y < B.Y) ? true : false;
        }
        public static bool operator >(Point A, Point B)
        {
            return (A.X > B.X) && (A.Y < B.Y) ? true : false;
        }

        //.///.////

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y);
        }
        public double SquareLength()
        {
            return X * X + Y * Y;
        }
        /// <summary>
        /// ¬озвращает true, если точка лежит в области, ограниченной пр€моугольником, заданным диагональю с концами в точках A и B
        /// </summary>
        /// <param name="A">¬ерхн€€ лева€ точка пр€моугольника</param>
        /// <param name="B">Ќижн€€ права€ точка пр€моугольника</param>
        /// <returns></returns>
        public bool Include(Point A, Point B)
        {
            Point k = new Point(X, Y);
            return (k >= A) && (k <= B);
        }
        public bool Include(Rectangle rect)
        {
            Point k = new Point(X, Y);
            Point A = new Point(rect.X, rect.Y);
            Point B = A + new Point(rect.Width, rect.Height);
            return (k >= A) && (k <= B);
        }
        public bool IncludeStrictly(Point A, Point B)
        {
            Point k = new Point(X, Y);
            return (k > A) && (k < B);
        }
        public bool IncludeStrictly(Rectangle rect)
        {
            Point k = new Point(X, Y);
            Point A = new Point(rect.X, rect.Y);
            Point B = A + new Point(rect.Width, rect.Height);
            return (k > A) && (k < B);
        }
        /// <summary>
        /// ѕоложительные значени€ угла - вращение по часовой стрелке, OX-вправо, OY-вниз
        /// </summary>
        /// <param name="Vect"></param>
        /// <param name="Angle"></param>
        /// <returns></returns>
        public static Point Rotate(Point Vect, int Angle)
        {
            if (Angle >= 360) Angle = Angle - Math.Sign(Angle) * (Angle / 360) * 360;
            if (Angle < 0) Angle = 360 - Angle;
            double rad = (double)Angle / 180 * Math.PI;
            var sin = Math.Sin(rad);
            var cos = Math.Cos(rad);
            return new Point((int)(Vect.X * cos - Vect.Y * sin), (int)(Vect.X * sin + Vect.Y * cos));
        }
        public Point Rotate(int Angle) => Rotate(new Point(X, Y), Angle);

        public static implicit operator System.Drawing.Point(Point A) => new System.Drawing.Point(A.X, A.Y);
        public static implicit operator Point(System.Drawing.Point A) => new Point(A.X, A.Y);
        public static implicit operator Point(Size A) => new Point(A.Width, A.Height);
        public static implicit operator Size(Point A) => new Size(A.X, A.Y);
        public static implicit operator Point(int a) => new Point(a, a);
    }
}
