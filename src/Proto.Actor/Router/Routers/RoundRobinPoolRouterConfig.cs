// -----------------------------------------------------------------------
// <copyright file="RoundRobinPoolRouterConfig.cs" company="Asynkron AB">
//      Copyright (C) 2015-2024 Asynkron AB All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Proto.Router.Routers;

internal record RoundRobinPoolRouterConfig : PoolRouterConfig
{
    private readonly ISenderContext _senderContext;

    public RoundRobinPoolRouterConfig(ISenderContext senderContext, int poolSize, Props routeeProps)
        : base(poolSize, routeeProps)
    {
        _senderContext = senderContext;
    }

    protected override RouterState CreateRouterState() => new RoundRobinRouterState(_senderContext);
}