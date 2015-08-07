using System;

namespace GiantBomb.Api
{
    public class GiantBombHttpException : Exception
    {
        public GiantBombHttpException(string content)
            : base("GiantBomb HTTP Exception, bad request from API: " + content)
        {

        }

        public GiantBombHttpException(string content, Exception innerEx)
            : base("GiantBomb HTTP Exception, bad request from API: " + content, innerEx)
        {

        }
    }
}