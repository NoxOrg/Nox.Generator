﻿// Generated

#nullable enable

using MediatR;
using Microsoft.EntityFrameworkCore;
using Nox.Application.Commands;
using Nox.Solution;
using Nox.Types;
using CryptocashApi.Infrastructure.Persistence;
using CryptocashApi.Domain;

namespace CryptocashApi.Application.Commands;

public record DeleteEmployeeByIdCommand(System.Int64 keyId) : IRequest<bool>;

public class DeleteEmployeeByIdCommandHandler: CommandBase, IRequestHandler<DeleteEmployeeByIdCommand, bool>
{
    public CryptocashApiDbContext DbContext { get; }

    public  DeleteEmployeeByIdCommandHandler(
        CryptocashApiDbContext dbContext,
        NoxSolution noxSolution, 
        IServiceProvider serviceProvider): base(noxSolution, serviceProvider)
    {
        DbContext = dbContext;
    }    

    public async Task<bool> Handle(DeleteEmployeeByIdCommand request, CancellationToken cancellationToken)
    {
        var keyId = CreateNoxTypeForKey<Employee,DatabaseNumber>("Id", request.keyId);

        var entity = await DbContext.Employees.FindAsync(keyId);
        if (entity == null || entity.Deleted == true)
        {
            return false;
        }

        entity.Delete();
        await DbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}