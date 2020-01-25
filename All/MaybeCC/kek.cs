using System;
using System.Runtime.CompilerServices;

namespace MaybeCC
{
	[AsyncMethodBuilder(typeof(MaybeAsyncMethodBuilder<>))]
	interface Option<T>
	{
	}
	
	sealed class None<T> : Option<T>
	{
		public static readonly None<T> Value = new None<T>();
	}

	sealed class Some<T> : Option<T>
	{
		public readonly T Item;
		public Some(T item) => Item = item;
		public static explicit operator T(Some<T> maybe) => maybe.Item;
	}

	static class Some
	{
		public static Some<T> Of<T>(T value) => new Some<T>(value);
	}

	class MaybeAsyncMethodBuilder<T>
	{
		Option<T> result = None<T>.Value;

		public static MaybeAsyncMethodBuilder<T> Create() => new MaybeAsyncMethodBuilder<T>();

		public void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			stateMachine.MoveNext();
		}   

		public Option<T> Task => result;
		public void SetResult(T result) => this.result = Some.Of(result);

		public void SetException(Exception ex)
		{
		}
		
		public void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		public void AwaitOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter, ref TStateMachine stateMachine)
			where TAwaiter : INotifyCompletion
			where TStateMachine : IAsyncStateMachine
		{
		}

		public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(ref TAwaiter awaiter,
			ref TStateMachine stateMachine)
			where TAwaiter : ICriticalNotifyCompletion
			where TStateMachine : IAsyncStateMachine
		{
		}
	}

	class MaybeAwaiter<T> : INotifyCompletion
	{
		Option<T> maybe;

		public MaybeAwaiter(Option<T> maybe) => this.maybe = maybe;

		public bool IsCompleted => maybe is Some<T>;

		public void OnCompleted(Action continuation)
		{
		}

		public T GetResult() => ((Some<T>) maybe).Item;
	}

	static class OptionExtensions
	{
		public static MaybeAwaiter<T> GetAwaiter<T>(this Option<T> maybe) => new MaybeAwaiter<T>(maybe);
	}
}