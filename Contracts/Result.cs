using System;

namespace DeviceDataApi.Contracts
{
	public class Result
	{
		public bool IsSuccess { get; }

		public bool IsFailure => !IsSuccess;

		public string Message { get; }

		protected Result(bool success, string message)
		{
			IsSuccess = success;
			Message = message;
		}

		public static Result Ok()
		{
			return new Result(true, null);
		}

		public static Result<T> Ok<T>()
		{
			return new Result<T>(default, true, null);
		}

		public static Result<T> Ok<T>(T value)
		{
			return new Result<T>(value, true, null);
		}

		public static Result Fail(string description)
		{
			return new Result(false, description);
		}

		public static Result<T> Fail<T>(string description)
		{
			return new Result<T>(default, false, description);
		}
	}

	public class Result<T> : Result
	{
		private T _data;
		public T Data
		{
			get
			{
				if (IsFailure)
					throw new InvalidOperationException("Cannot get value when Result is in error");

				return _data;
			}
			private set
			{
				if (value == null)
					throw new InvalidOperationException("Cannot set value to null");

				_data = value;
			}
		}

		protected internal Result(T value, bool success, string message)
			: base(success, message)
		{
			if (success)
			{
				if (value != null)
					Data = value;
			}
		}
	}
}
