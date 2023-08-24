﻿using Microsoft.Extensions.DependencyInjection;

using Nox.Domain;
using Nox.Factories;
using Nox.Solution;

namespace Nox.Application.Commands;

/// <summary>
/// Base Implementation for aNox Command
/// </summary>
public abstract class CommandBase<TRequest, TEntity>: INoxCommand where TEntity: IEntity  
{
    protected NoxSolution NoxSolution { get; }
    protected IServiceProvider ServiceProvider { get; }
    public CommandBase(NoxSolution noxSolution, IServiceProvider serviceProvider)
    {
        NoxSolution = noxSolution;
        ServiceProvider = serviceProvider;
    }

    protected N? CreateNoxTypeForKey<E, N>(string keyName, dynamic? value) where N : Types.INoxType
    {
        var entityDefinition = GetEntityDefinition<E>();
        var key = entityDefinition.Keys!.Single(entity => entity.Name == keyName);

        var typeFactory = ServiceProvider.GetService<INoxTypeFactory<N>>();
        return typeFactory!.CreateNoxType(key, value);
    }

    protected Entity GetEntityDefinition<E>()
    {
        return NoxSolution.Domain!.GetEntityByName(typeof(E).Name);
    }

    /// <summary>
    /// Executing the command handler, use this method to override or update the request
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    protected virtual void OnExecuting(TRequest request)
    {
        
    }

    /// <summary>
    /// Command handler completed
    /// Use this method to override, update, validate or other run custom logic regarding the updated/created Entity
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    protected virtual void OnCompleted(TEntity entity)
    {
        
    }
}

