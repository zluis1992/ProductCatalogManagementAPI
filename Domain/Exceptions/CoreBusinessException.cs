﻿using System.Runtime.Serialization;

namespace Domain.Exceptions;

[Serializable]
public class CoreBusinessException : Exception
{
    public CoreBusinessException()
    {
    }

    public CoreBusinessException(string message) : base(message)
    {
    }

    public CoreBusinessException(string message, Exception inner) : base(message, inner)
    {
    }

    [Obsolete("Obsolete")]
    protected CoreBusinessException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
