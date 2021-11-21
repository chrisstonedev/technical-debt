using System.Runtime.Serialization;

namespace UiApprovalTest.TestFramework
{
    [Serializable]
    public class AssertException : Exception
    {
        public AssertException()
        {
        }

        public AssertException(string message) : base(message)
        {
        }

        public AssertException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AssertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}