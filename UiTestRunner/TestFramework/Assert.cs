namespace UiTestRunner.TestFramework
{
    public static class Assert
    {
        internal static void AreEqual(byte[] actualBytes, byte[] expectedBytes)
        {
            if (actualBytes.Length != expectedBytes.Length)
            {
                throw new AssertException($"Byte arrays do not match; expected length is {expectedBytes.Length}, but actual length is {actualBytes.Length}.");
            }
            for (int i = 0; i < actualBytes.Length; i++)
            {
                if (actualBytes[i] != expectedBytes[i])
                {
                    throw new AssertException($"Byte arrays differ at position {i}; expected[{i}] is {expectedBytes[i]}, but actual[{i}] is {actualBytes[i]}.");
                }
            }

            return;
        }
    }
}