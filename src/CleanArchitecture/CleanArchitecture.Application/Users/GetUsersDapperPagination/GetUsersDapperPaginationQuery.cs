using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchitecture.Application.Users.GetUsersDapperPagination;

public sealed record GetUsersDapperPaginationQuery() 
        : PaginationParams, IQuery<PagedDapperResults<UserPaginationData>>
{
    
}