// -----------------------------------------------------------------------
// <copyright file="GrainResponseMessage.cs" company="Asynkron AB">
//      Copyright (C) 2015-2024 Asynkron AB All rights reserved
// </copyright>
// -----------------------------------------------------------------------

using System;
using Google.Protobuf;
using Proto.Diagnostics;
using Proto.Remote;

namespace Proto.Cluster;

/// <summary>
///     A response message wrapper used for code-generated virtual actors (grains).
/// </summary>
/// <param name="ResponseMessage">Wrapped message</param>
public record GrainResponseMessage(IMessage? ResponseMessage) : IRootSerializable, IDiagnosticsTypeName
{
    public IRootSerialized Serialize(ActorSystem system)
    {
        if (ResponseMessage is null)
        {
            return new GrainResponse();
        }

        var ser = system.Serialization();
        var (data, typeName, serializerId) = ser.Serialize(ResponseMessage);
#if DEBUG
        if (serializerId != Serialization.SERIALIZER_ID_PROTOBUF)
        {
            throw new Exception($"Grains must use ProtoBuf types: {ResponseMessage.GetType().FullName}");
        }
#endif
        return new GrainResponse
        {
            MessageData = data,
            MessageTypeName = typeName
        };
    }

    public string GetTypeName()
    {
        var m = ResponseMessage?.GetType().Name ?? "null";

        return $"GrainRequest({m})";
    }
}