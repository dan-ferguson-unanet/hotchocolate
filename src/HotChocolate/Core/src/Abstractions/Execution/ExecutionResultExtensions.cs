using System;

namespace HotChocolate.Execution;

/// <summary>
/// Helper methods for <see cref="IExecutionResult"/>.
/// </summary>
public static class ExecutionResultExtensions
{
    /// <summary>
    /// Registers a cleanup task for execution resources bound to this execution result.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IExecutionResult"/>.
    /// </param>
    /// <param name="clean">
    /// A cleanup task that will be executed when this result is disposed.
    /// </param>
    public static void RegisterForCleanup(this IExecutionResult result, Action clean)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (clean is null)
        {
            throw new ArgumentNullException(nameof(clean));
        }

        result.RegisterForCleanup(() =>
        {
            clean();
            return default;
        });
    }

    /// <summary>
    /// Registers a resource that needs to be disposed when the result is being disposed.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IExecutionResult"/>.
    /// </param>
    /// <param name="disposable">
    /// The resource that needs to be disposed.
    /// </param>
    public static void RegisterForCleanup(this IExecutionResult result, IDisposable disposable)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (disposable is null)
        {
            throw new ArgumentNullException(nameof(disposable));
        }

        result.RegisterForCleanup(disposable.Dispose);
    }

    /// <summary>
    /// Registers a resource that needs to be disposed when the result is being disposed.
    /// </summary>
    /// <param name="result">
    /// The <see cref="IExecutionResult"/>.
    /// </param>
    /// <param name="disposable">
    /// The resource that needs to be disposed.
    /// </param>
    public static void RegisterForCleanup(this IExecutionResult result, IAsyncDisposable disposable)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result));
        }

        if (disposable is null)
        {
            throw new ArgumentNullException(nameof(disposable));
        }

        result.RegisterForCleanup(disposable.DisposeAsync);
    }
}
