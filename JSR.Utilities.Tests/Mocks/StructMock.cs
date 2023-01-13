using System.Diagnostics.CodeAnalysis;

namespace JSR.Utilities.Tests.Mocks
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit test")]
    public struct StructMock
    {
        public StructMock(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public static bool operator ==(StructMock left, StructMock right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(StructMock left, StructMock right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public override bool Equals([NotNullWhen(true)] object obj)
        {
            if (obj == null || obj.GetType() != typeof(StructMock))
            {
                return false;
            }

            return Equals((StructMock)obj);
        }

        public bool Equals(StructMock other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
