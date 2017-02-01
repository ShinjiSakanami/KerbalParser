using System;

namespace KerbalParser
{
    public struct Vector2
    {
        public double x;

        public double y;

        public double this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return this.x;
                }
                if (index != 1)
                {
                    throw new IndexOutOfRangeException("Invalid Vector2 index!");
                }
                return this.y;
            }
            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                    }
                    this.y = value;
                }
                else
                {
                    this.x = value;
                }
            }
        }

        public static Vector2 Zero
        {
            get
            {
                return new Vector2(0.0, 0.0);
            }
        }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void Set(double new_x, double new_y)
        {
            this.x = new_x;
            this.y = new_y;
        }

        public override string ToString()
        {
            return String.Format("({0:F1}, {1:F1})", new object[]
            {
                this.x,
                this.y
            });
        }

        public string ToString(string format)
        {
            return String.Format("({0}, {1})", new object[]
            {
                this.x.ToString(format),
                this.y.ToString(format)
            });
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2))
            {
                return false;
            }
            Vector2 vector = (Vector2)other;
            return this.x.Equals(vector.x) && this.y.Equals(vector.y);
        }
    }
}
