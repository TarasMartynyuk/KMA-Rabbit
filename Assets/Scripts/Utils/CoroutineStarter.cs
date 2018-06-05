namespace Utils
{
    /// <summary>
    /// provides my non-monobehaviour classes ability to start coroutines
    /// being a singleton, the object guarantees that the coroutine started on him
    /// will not be killed before completion on this objects destruction
    /// </summary>
    public class CoroutineStarter : Singleton<CoroutineStarter> 
    {}
}
