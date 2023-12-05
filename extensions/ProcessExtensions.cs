using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MCSMLauncher.extensions
{
    /// <summary>
    /// This class contains extension methods for the Process class.
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Waits asynchronously for the process to exit.
        /// </summary>
        /// <param name="process">The process to wait for cancellation.</param>
        /// <param name="cancellationToken">
        /// A cancellation token. If invoked, the task will return
        /// immediately as canceled.
        /// </param>
        /// <returns>A Task representing waiting for the process to end.</returns>
        public static Task WaitForExitAsync(this Process process, CancellationToken cancellationToken = default)
        {
            if (process.HasExited) return Task.CompletedTask;

            TaskCompletionSource<object> tcs = new ();
            process.EnableRaisingEvents = true;
            process.Exited += (_, _) => tcs.TrySetResult(null);
            if (cancellationToken != default) cancellationToken.Register(() => tcs.SetCanceled());

            return process.HasExited ? Task.CompletedTask : tcs.Task;
        }
    }
}