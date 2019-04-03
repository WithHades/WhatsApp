using System;

namespace Xzy.Buffer
{
    [Serializable]
    public class UnableToAllocateBufferException : Exception
    {
        public UnableToAllocateBufferException()
            : base("Cannot allocate buffer after few trials.")
        {
        }
    }
}
