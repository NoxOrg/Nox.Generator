﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using SampleWebApp.Infrastructure.Persistence;
using SampleWebApp.Domain;

namespace SampleWebApp.Application.Commands;

public record DeleteAllNoxTypeByIdCommand(System.Int64 keyId, System.String keyTextId) : IRequest<bool>;

public class DeleteAllNoxTypeByIdCommandHandler: CommandBase, IRequestHandler<DeleteAllNoxTypeByIdCommand, bool>
{
    public SampleWebAppDbContext DbContext { get; }

    public  DeleteAllNoxTypeByIdCommandHandler(
        SampleWebAppDbContext dbContext,
        NoxSolution noxSolution, 
        IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
    {
        DbContext = dbContext;
    }    

    public async Task<bool> Handle(DeleteAllNoxTypeByIdCommand request, CancellationToken cancellationToken)
    {
        var keyId = CreateNoxTypeForKey<AllNoxType,DatabaseNumber>("Id", request.keyId);
        var keyTextId = CreateNoxTypeForKey<AllNoxType,Text>("TextId", request.keyTextId);

        var entity = await DbContext.AllNoxTypes.FindAsync(keyId, keyTextId);
        if (entity is null || entity.IsDeleted.Value == true)
        {
            return false;
        }
        entity.Deleted();
        await DbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}