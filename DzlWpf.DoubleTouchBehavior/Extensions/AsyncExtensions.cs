using System;
using System.Threading;
using System.Threading.Tasks;

namespace DzlWpf.DoubleTouchBehavior.Extensions
{
    public static class AsyncExtensions
    {
        public static async Task<T> WithCancellation<T>(this TaskCompletionSource<T> tasktcs, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (tasktcs.Task != await Task.WhenAny(tasktcs.Task, tcs.Task))
                {
                    tasktcs.TrySetCanceled();
                    throw new OperationCanceledException(cancellationToken);
                }
            }

            return tasktcs.Task.Result;
        }
    }
}
