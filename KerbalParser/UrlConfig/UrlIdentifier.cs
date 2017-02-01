using System;

namespace KerbalParser
{
    public class UrlIdentifier
    {
        private string _url;

        private string[] _urlSplit;

        private int _urlDepth;

        public string this[int index]
        {
            get
            {
                return this._urlSplit[index];
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this.ConstructUrl(value);
            }
        }

        public string[] UrlSplit
        {
            get
            {
                return this._urlSplit;
            }
        }

        public int UrlDepth
        {
            get
            {
                return this._urlDepth;
            }
        }

        public UrlIdentifier()
        {
            this._url = string.Empty;
            this._urlSplit = new string[0];
            this._urlDepth = -1;
        }

        public UrlIdentifier(string url)
        {
            this.ConstructUrl(url);
        }

        private void ConstructUrl(string url)
        {
            this._url = url;
            this._urlSplit = UrlIdentifier.UrlSpliter(url);
            this._urlDepth = this._urlSplit.Length - 1;
        }

        private static string[] UrlSpliter(string url)
        {
            url = url.Trim(new char[]
            {
                '/',
                ' ',
                '\n',
                '\r',
                '\t'
            });
            return url.Split(new char[]
            {
                '/',
                ' ',
                '.'
            }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
