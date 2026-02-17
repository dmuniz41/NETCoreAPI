using System;

namespace Domain.Shared;

public interface IValidationResult
{
  public static readonly Error ValidationError = new("Validation Error", "A validation error occurred.");

  Error[] Errors { get; }
}
