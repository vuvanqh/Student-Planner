using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentPlanner.Core.Errors;

public class IdentityOperationException : Exception
{
    public IEnumerable<string> Errors { get; }

    public IdentityOperationException(IEnumerable<string> errors)
        : base("Identity operation failed")
    {

        Errors = errors;
    }
}
