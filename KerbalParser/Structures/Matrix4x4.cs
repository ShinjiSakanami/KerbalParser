using System;

namespace KerbalParser
{
    public struct Matrix4x4
    {
        public double m00;

        public double m10;

        public double m20;

        public double m30;

        public double m01;

        public double m11;

        public double m21;

        public double m31;

        public double m02;

        public double m12;

        public double m22;

        public double m32;

        public double m03;

        public double m13;

        public double m23;

        public double m33;

        public double this[int row, int column]
        {
            get
            {
                return this[row + column * 4];
            }
            set
            {
                this[row + column * 4] = value;
            }
        }

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.m00;
                    case 1:
                        return this.m10;
                    case 2:
                        return this.m20;
                    case 3:
                        return this.m30;
                    case 4:
                        return this.m01;
                    case 5:
                        return this.m11;
                    case 6:
                        return this.m21;
                    case 7:
                        return this.m31;
                    case 8:
                        return this.m02;
                    case 9:
                        return this.m12;
                    case 10:
                        return this.m22;
                    case 11:
                        return this.m32;
                    case 12:
                        return this.m03;
                    case 13:
                        return this.m13;
                    case 14:
                        return this.m23;
                    case 15:
                        return this.m33;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.m00 = value;
                        break;
                    case 1:
                        this.m10 = value;
                        break;
                    case 2:
                        this.m20 = value;
                        break;
                    case 3:
                        this.m30 = value;
                        break;
                    case 4:
                        this.m01 = value;
                        break;
                    case 5:
                        this.m11 = value;
                        break;
                    case 6:
                        this.m21 = value;
                        break;
                    case 7:
                        this.m31 = value;
                        break;
                    case 8:
                        this.m02 = value;
                        break;
                    case 9:
                        this.m12 = value;
                        break;
                    case 10:
                        this.m22 = value;
                        break;
                    case 11:
                        this.m32 = value;
                        break;
                    case 12:
                        this.m03 = value;
                        break;
                    case 13:
                        this.m13 = value;
                        break;
                    case 14:
                        this.m23 = value;
                        break;
                    case 15:
                        this.m33 = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid matrix index!");
                }
            }
        }

        public static Matrix4x4 Zero
        {
            get
            {
                return new Matrix4x4
                {
                    m00 = 0.0,
                    m01 = 0.0,
                    m02 = 0.0,
                    m03 = 0.0,
                    m10 = 0.0,
                    m11 = 0.0,
                    m12 = 0.0,
                    m13 = 0.0,
                    m20 = 0.0,
                    m21 = 0.0,
                    m22 = 0.0,
                    m23 = 0.0,
                    m30 = 0.0,
                    m31 = 0.0,
                    m32 = 0.0,
                    m33 = 0.0
                };
            }
        }

        public static Matrix4x4 Identity
        {
            get
            {
                return new Matrix4x4
                {
                    m00 = 1.0,
                    m01 = 0.0,
                    m02 = 0.0,
                    m03 = 0.0,
                    m10 = 0.0,
                    m11 = 1.0,
                    m12 = 0.0,
                    m13 = 0.0,
                    m20 = 0.0,
                    m21 = 0.0,
                    m22 = 1.0,
                    m23 = 0.0,
                    m30 = 0.0,
                    m31 = 0.0,
                    m32 = 0.0,
                    m33 = 1.0
                };
            }
        }

        public Vector4 GetColumn(int i)
        {
            return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
        }

        public Vector4 GetRow(int i)
        {
            return new Vector4(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
        }

        public void SetColumn(int i, Vector4 v)
        {
            this[0, i] = v.x;
            this[1, i] = v.y;
            this[2, i] = v.z;
            this[3, i] = v.w;
        }

        public void SetRow(int i, Vector4 v)
        {
            this[i, 0] = v.x;
            this[i, 1] = v.y;
            this[i, 2] = v.z;
            this[i, 3] = v.w;
        }

        public override string ToString()
        {
            return String.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[]
            {
                this.m00,
                this.m01,
                this.m02,
                this.m03,
                this.m10,
                this.m11,
                this.m12,
                this.m13,
                this.m20,
                this.m21,
                this.m22,
                this.m23,
                this.m30,
                this.m31,
                this.m32,
                this.m33
            });
        }

        public string ToString(string format)
        {
            return String.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
            {
                this.m00.ToString(format),
                this.m01.ToString(format),
                this.m02.ToString(format),
                this.m03.ToString(format),
                this.m10.ToString(format),
                this.m11.ToString(format),
                this.m12.ToString(format),
                this.m13.ToString(format),
                this.m20.ToString(format),
                this.m21.ToString(format),
                this.m22.ToString(format),
                this.m23.ToString(format),
                this.m30.ToString(format),
                this.m31.ToString(format),
                this.m32.ToString(format),
                this.m33.ToString(format)
            });
        }

        public override int GetHashCode()
        {
            return this.GetColumn(0).GetHashCode() ^ this.GetColumn(1).GetHashCode() << 2 ^ this.GetColumn(2).GetHashCode() >> 2 ^ this.GetColumn(3).GetHashCode() >> 1;
        }

        public override bool Equals(object other)
        {
            if (!(other is Matrix4x4))
            {
                return false;
            }
            Matrix4x4 matrix4x = (Matrix4x4)other;
            return this.GetColumn(0).Equals(matrix4x.GetColumn(0)) && this.GetColumn(1).Equals(matrix4x.GetColumn(1)) && this.GetColumn(2).Equals(matrix4x.GetColumn(2)) && this.GetColumn(3).Equals(matrix4x.GetColumn(3));
        }
    }
}
