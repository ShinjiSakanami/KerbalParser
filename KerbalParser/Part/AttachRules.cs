using System;

namespace KerbalParser
{
    public class AttachRules
    {
        public bool stack;

        public bool srfAttach;

        public bool allowStack;

        public bool allowSftAttach;

        public bool allowCollision;

        public bool allowDock;

        public bool allowRotate;

        public bool allowRoot;

        public static AttachRules Parse(string value)
        {
            string[] array = value.Split(new char[]
            {
                ','
            });
            if (array.Length < 5)
            {
                throw new Exception("Attach rules are 5 to 8 ints, either ones or zeros, separated by commas!");
            }
            AttachRules attachRules = new AttachRules();
            if (array[0] == "1")
            {
                attachRules.stack = true;
            }
            if (array[1] == "1")
            {
                attachRules.srfAttach = true;
            }
            if (array[2] == "1")
            {
                attachRules.allowStack = true;
            }
            if (array[3] == "1")
            {
                attachRules.allowSftAttach = true;
            }
            if (array[4] == "1")
            {
                attachRules.allowCollision = true;
            }
            if (array.Length >= 6)
            {
                if (array[5] == "1")
                {
                    attachRules.allowDock = true;
                }
            }
            if (array.Length >= 7)
            {
                if (array[6] == "1")
                {
                    attachRules.allowRotate = true;
                }
            }
            if (array.Length >= 8)
            {
                if (array[7] == "1")
                {
                    attachRules.allowRoot = true;
                }
            }
            return attachRules;
        }

        public override string ToString()
        {
            string text = string.Empty;
            if (this.stack)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.srfAttach)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.allowStack)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.allowSftAttach)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.allowCollision)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.allowDock)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.allowRotate)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            text += ",";
            if (this.allowRoot)
            {
                text += "1";
            }
            else
            {
                text += "0";
            }
            return text;
        }
    }
}
