using System;
using System.Globalization;
using System.IO;

namespace KerbalParser
{
    public delegate T ParserMethod<T>(string value);

    public class KPUtil
    {
        public static string DateTimeNow(string format)
        {
            return DateTime.Now.ToString(format, CultureInfo.InvariantCulture);
        }

        public static string PathCombine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public static string GetOrCreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static string GetPartName(string configPartID)
        {
            int length = configPartID.IndexOf('_');
            return configPartID.Substring(0, length);
        }

        public static T[] ParseArray<T>(string arrayString, ParserMethod<T> parser)
        {
            string[] array = arrayString.Split(new char[]
            {
                ';'
            });
            T[] array2 = new T[array.Length];
            int num = array.Length;
            for (int i = 0; i < num; i++)
            {
                array2[i] = parser(array[i].Trim());
            }
            return array2;
        }

        public static Enum ParseEnum(Type fieldType, string enumString)
        {
            try
            {
                return (Enum)Enum.Parse(fieldType, enumString, true);
            }
            catch (ArgumentException)
            {
                Debug.LogWarning("Warning: '" + enumString + "' is not a valid value of " + fieldType.Name);
                return null;
            }
        }

        public static T ParseEnum<T>(string enumString)
        {
            try
            {
                return (T)Enum.Parse(typeof(T), enumString, true);
            }
            catch (ArgumentException)
            {
                Debug.LogWarning("Warning: '" + enumString + "' is not a valid value of " + typeof(T).Name);
                return default(T);
            }
        }

        public static double ParseDouble(string doubleString)
        {
            return double.Parse(doubleString, CultureInfo.InvariantCulture);
        }

        public static int ParseInt(string intString)
        {
            return int.Parse(intString, CultureInfo.InvariantCulture);
        }

        public static Quaternion ParseQuaternion(string quaternionString)
        {
            string[] array = quaternionString.Split(new char[]
            {
                ','
            });
            if (array.Length < 4)
            {
                Debug.LogWarning("WARNING: Quaternion entry is not formatted properly! Proper format for Quaternion is x,y,z,w");
                return Quaternion.Identity;
            }
            return new Quaternion(KPUtil.ParseDouble(array[0]), KPUtil.ParseDouble(array[1]), KPUtil.ParseDouble(array[2]), KPUtil.ParseDouble(array[3]));
        }

        public static Quaternion ParseQuaternion(string x, string y, string z, string w)
        {
            return new Quaternion(KPUtil.ParseDouble(x), KPUtil.ParseDouble(y), KPUtil.ParseDouble(z), KPUtil.ParseDouble(w));
        }

        public static Vector2 ParseVector2(string vectorString)
        {
            string[] array = vectorString.Split(new char[]
            {
                ','
            });
            if (array.Length < 2)
            {
                Debug.LogWarning("WARNING: Vector2 entry is not formatted properly! Proper format for Vector2 is x,y");
                return Vector2.Zero;
            }
            return new Vector2(KPUtil.ParseDouble(array[0]), KPUtil.ParseDouble(array[1]));
        }

        public static Vector2 ParseVector2(string x, string y)
        {
            return new Vector2(KPUtil.ParseDouble(x), KPUtil.ParseDouble(y));
        }

        public static Vector3 ParseVector3(string vectorString)
        {
            string[] array = vectorString.Split(new char[]
            {
                ','
            });
            if (array.Length < 3)
            {
                Debug.LogWarning("WARNING: Vector3 entry is not formatted properly! Proper format for Vector3 is x,y,z");
                return Vector3.Zero;
            }
            return new Vector3(KPUtil.ParseDouble(array[0]), KPUtil.ParseDouble(array[1]), KPUtil.ParseDouble(array[2]));
        }

        public static Vector3 ParseVector3(string x, string y, string z)
        {
            return new Vector3(KPUtil.ParseDouble(x), KPUtil.ParseDouble(y), KPUtil.ParseDouble(z));
        }

        public static Vector4 ParseVector4(string vectorString)
        {
            string[] array = vectorString.Split(new char[]
            {
                ','
            });
            if (array.Length < 4)
            {
                Debug.LogWarning("WARNING: Vector4 entry is not formatted properly! Proper format for Vector4 is x,y,z,w");
                return Vector4.Zero;
            }
            return new Vector4(KPUtil.ParseDouble(array[0]), KPUtil.ParseDouble(array[1]), KPUtil.ParseDouble(array[2]), KPUtil.ParseDouble(array[3]));
        }

        public static Vector4 ParseVector4(string x, string y, string z, string w)
        {
            return new Vector4(KPUtil.ParseDouble(x), KPUtil.ParseDouble(y), KPUtil.ParseDouble(z), KPUtil.ParseDouble(w));
        }

        public static Matrix4x4 ParseMatrix4x4(string matrixString)
        {
            string[] array = matrixString.Split(new char[]
            {
                ','
            });
            if (array.Length < 16)
            {
                Debug.LogWarning("WARNING: Matrix4x4 entry is not formatted properly! Proper format for Matrix4x4 is 16 csv floats (m00,m01,m02,m03,m10,m11..m33)");
                return Matrix4x4.Identity;
            }
            Matrix4x4 matrix = Matrix4x4.Identity;
            matrix.m00 = KPUtil.ParseDouble(array[0]);
            matrix.m01 = KPUtil.ParseDouble(array[1]);
            matrix.m02 = KPUtil.ParseDouble(array[2]);
            matrix.m03 = KPUtil.ParseDouble(array[3]);
            matrix.m10 = KPUtil.ParseDouble(array[4]);
            matrix.m11 = KPUtil.ParseDouble(array[5]);
            matrix.m12 = KPUtil.ParseDouble(array[6]);
            matrix.m13 = KPUtil.ParseDouble(array[7]);
            matrix.m20 = KPUtil.ParseDouble(array[8]);
            matrix.m21 = KPUtil.ParseDouble(array[9]);
            matrix.m22 = KPUtil.ParseDouble(array[10]);
            matrix.m23 = KPUtil.ParseDouble(array[11]);
            matrix.m30 = KPUtil.ParseDouble(array[12]);
            matrix.m31 = KPUtil.ParseDouble(array[13]);
            matrix.m32 = KPUtil.ParseDouble(array[14]);
            matrix.m33 = KPUtil.ParseDouble(array[15]);
            return matrix;
        }

        public static Color ParseColor(string colorString)
        {
            string[] array = colorString.Split(new char[]
            {
                ','
            });
            if (array.Length < 3)
            {
                Debug.LogWarning("WARNING: Color entry is not formatted properly! Proper format for Colors is r,g,b{,a}");
                return Color.White;
            }
            if (array.Length > 3)
            {
                return new Color(KPUtil.ParseDouble(array[0]), KPUtil.ParseDouble(array[1]), KPUtil.ParseDouble(array[2]), KPUtil.ParseDouble(array[3]));
            }
            return new Color(KPUtil.ParseDouble(array[0]), KPUtil.ParseDouble(array[1]), KPUtil.ParseDouble(array[2]));
        }

        public static string WriteObject(object value)
        {
            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        public static string WriteDouble(double d)
        {
            return d.ToString("G9", CultureInfo.InvariantCulture);
        }

        public static string WriteInt(int i)
        {
            return i.ToString("G", CultureInfo.InvariantCulture);
        }

        public static string WriteArray<T>(T[] array) where T : IConvertible
        {
            string text = string.Empty;
            int num = array.Length;
            for (int i = 0; i < num; i++)
            {
                text += array[i].ToString();
                if (i < array.Length - 1)
                {
                    text += "; ";
                }
            }
            return text;
        }

        public static string WriteQuaternion(Quaternion quaternion)
        {
            return string.Concat(new string[]
            {
                quaternion.x.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                quaternion.y.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                quaternion.z.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                quaternion.w.ToString("G9", CultureInfo.InvariantCulture)
            });
        }

        public static string WriteVector(Vector2 vector)
        {
            return string.Concat(new string[]
            {
                vector.x.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                vector.y.ToString("G9", CultureInfo.InvariantCulture)
            });
        }

        public static string WriteVector(Vector3 vector)
        {
            return string.Concat(new string[]
            {
                vector.x.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                vector.y.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                vector.z.ToString("G9", CultureInfo.InvariantCulture)
            });
        }

        public static string WriteVector(Vector4 vector)
        {
            return string.Concat(new string[]
            {
                vector.x.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                vector.y.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                vector.z.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                vector.w.ToString("G9", CultureInfo.InvariantCulture)
            });
        }

        public static string WriteColor(Color color)
        {
            return string.Concat(new string[]
            {
                color.r.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                color.g.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                color.b.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                color.a.ToString("G9", CultureInfo.InvariantCulture),
            });
        }

        public static string WriteMatrix4x4(Matrix4x4 matrix)
        {
            return string.Concat(new string[]
            {
                matrix.m00.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m01.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m02.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m03.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m10.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m11.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m12.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m13.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m20.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m21.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m22.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m23.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m30.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m31.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m32.ToString("G9", CultureInfo.InvariantCulture),
                ",",
                matrix.m33.ToString("G9", CultureInfo.InvariantCulture)
            });
        }
    }
}
