using System;

namespace KerbalParser
{
    public struct Color
    {
        public double r;

        public double g;

        public double b;

        public double a;

        public static Color Red
        {
            get
            {
                return new Color(1.0, 0.0, 0.0, 1.0);
            }
        }

        public static Color Green
        {
            get
            {
                return new Color(0.0, 1.0, 0.0, 1.0);
            }
        }

        public static Color Blue
        {
            get
            {
                return new Color(0.0, 0.0, 1.0, 1.0);
            }
        }

        public static Color Black
        {
            get
            {
                return new Color(0.0, 0.0, 0.0, 1.0);
            }
        }

        public static Color White
        {
            get
            {
                return new Color(1.0, 1.0, 1.0, 1.0);
            }
        }

        public static Color Yellow
        {
            get
            {
                return new Color(1.0, 1.0, 0.0, 1.0);
            }
        }

        public static Color Cyan
        {
            get
            {
                return new Color(0.0, 1.0, 1.0, 1.0);
            }
        }

        public static Color Magenta
        {
            get
            {
                return new Color(1.0, 0.0, 1.0, 1.0);
            }
        }

        public static Color Gray
        {
            get
            {
                return new Color(0.5, 0.5, 0.5, 1.0);
            }
        }

        public static Color Grey
        {
            get
            {
                return new Color(0.5, 0.5, 0.5, 1.0);
            }
        }

        public static Color Clear
        {
            get
            {
                return new Color(0.0, 0.0, 0.0, 0.0);
            }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.r;
                    case 1:
                        return this.g;
                    case 2:
                        return this.b;
                    case 3:
                        return this.a;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.r = value;
                        break;
                    case 1:
                        this.g = value;
                        break;
                    case 2:
                        this.b = value;
                        break;
                    case 3:
                        this.a = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        public Color(double r, double g, double b, double a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public Color(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 1.0;
        }

        public void Set(double new_r, double new_g, double new_b, double new_a)
        {
            this.r = new_r;
            this.g = new_g;
            this.b = new_b;
            this.a = new_a;
        }

        public override string ToString()
        {
            return String.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", new object[]
            {
                this.r,
                this.g,
                this.b,
                this.a
            });
        }

        public string ToString(string format)
        {
            return String.Format("RGBA({0}, {1}, {2}, {3})", new object[]
            {
                this.r.ToString(format),
                this.g.ToString(format),
                this.b.ToString(format),
                this.a.ToString(format)
            });
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (!(other is Color))
            {
                return false;
            }
            Color color = (Color)other;
            return this.r.Equals(color.r) && this.g.Equals(color.g) && this.b.Equals(color.b) && this.a.Equals(color.a);
        }
    }
}
