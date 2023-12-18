namespace mcsm.common.interfaces
{
    /// <summary>
    /// Implements a single contractual method "RunTask" that runs a task in the background.
    /// </summary>
    public interface IBackgroundRunner
    {
        public void RunTask();
    }
}