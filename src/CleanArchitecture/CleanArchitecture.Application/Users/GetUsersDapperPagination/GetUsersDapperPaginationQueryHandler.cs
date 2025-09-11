using System.Text;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Users.GetUsersPagination;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Users;
using Dapper;

namespace CleanArchitecture.Application.Users.GetUsersDapperPagination;

internal sealed class GetUsersDapperPaginationQueryHandler

    : IQueryHandler<GetUsersDapperPaginationQuery, PagedDapperResults<UserPaginationData>>
{
    private readonly ISqlConnectionFactory _sqlconnectionFactory;

    public GetUsersDapperPaginationQueryHandler(ISqlConnectionFactory sqlconnectionFactory)
    {
        _sqlconnectionFactory = sqlconnectionFactory;
    }

    public async Task<Result<PagedDapperResults<UserPaginationData>>> Handle(GetUsersDapperPaginationQuery request, CancellationToken cancellationToken)
    {
        using var connection = _sqlconnectionFactory.CreateConnection();

        var builder = new StringBuilder("""
            SELECT
                usr.email,
                rl.name AS role,
                p.nombre AS permniso

            FROM users usr
                LEFT JOIN users_roles usrl
                    ON usr.id = usrl.user_id
                LEFT JOIN roles rl
                    ON usrl.role_id = rl.id
                LEFT JOIN roles_permissions rp
                    ON rl.id = rp.role_id
                LEFT JOIN permissions p
                    ON rp.permission_id = p.id
        """);

        var search = string.Empty;
        var whereStatement = string.Empty;

        if (!string.IsNullOrEmpty(request.Search))
        {
            search = "%" + EncodeForLike(request.Search) + "%";
            whereStatement = " WHERE rl.name LIKE @search ";
            builder.AppendLine(whereStatement);
        }

        var orderBy = request.OrderBy;
        if (!string.IsNullOrEmpty(orderBy))
        {
            var orderStatement = string.Empty;
            var orderAsc = request.OrderAsc ? "ASC" : "DESC";
            switch (orderBy)
            {
                case "email": orderStatement = $" ORDER BY usr.email {orderAsc}"; break;
                case "role": orderStatement = $" ORDER BY rl.name {orderAsc}"; break;
                default: orderStatement = $" ORDER BY p.nombre {orderAsc}"; break;
            }
            builder.AppendLine(orderStatement);
        }

        builder.AppendLine(" LIMIT @PageSize OFFSET @Offset;");



        //count query
        builder.AppendLine("""
            SELECT
                COUNT(*)

            FROM users usr
                LEFT JOIN users_roles usrl
                    ON usr.id = usrl.user_id
                LEFT JOIN roles rl
                    ON usrl.role_id = rl.id
                LEFT JOIN roles_permissions rp
                    ON rl.id = rp.role_id
                LEFT JOIN permissions p
                    ON rp.permission_id = p.id        
        
        """);
        builder.AppendLine(whereStatement);
        builder.AppendLine(";");

        var offset = (request.PageNumber - 1) * request.PageSize;
        var sql = builder.ToString();
        using var multi = await connection.QueryMultipleAsync(sql, new
        {
            PageSize = request.PageSize,
            Offset = offset,
            Search = search
        });

        var items = await multi.ReadAsync<UserPaginationData>().ConfigureAwait(false);
        var totalItems = await multi.ReadFirstAsync<int>().ConfigureAwait(false);

        var result = new PagedDapperResults<UserPaginationData>(totalItems, request.PageNumber, request.PageSize)
        {
            Items = items,
        };

        return result;
    }


    private string EncodeForLike(string search)
    {
        return search.Replace("[", "[]]").Replace("%", "[%]");
    }
}