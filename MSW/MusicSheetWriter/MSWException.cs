using System;

namespace MusicSheetWriter
{
    internal class MSWException : Exception
    {
        public MSWException()
        {
        }

        public MSWException(string message) : base(message)
        {
        }

        public MSWException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}