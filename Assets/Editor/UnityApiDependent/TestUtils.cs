using System;

namespace UnityApiDependent
{
    static class TestUtils
    {
        public static void EmulateUpdate(Action onUpdate, int times)
        {
            for(int i = 0; i < times; i++)
            {
                onUpdate.Invoke();
            }
        }
    }
}
