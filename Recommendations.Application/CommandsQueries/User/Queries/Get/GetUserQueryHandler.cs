using MediatR;
using Microsoft.EntityFrameworkCore;
using Recommendations.Application.Common.Interfaces;

namespace Recommendations.Application.CommandsQueries.User.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Domain.User>
{
    private readonly IRecommendationsDbContext _context;

    public GetUserQueryHandler(IRecommendationsDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.User> Handle(GetUserQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(r => r.Id == request.UserId, cancellationToken);
        if (user is null)
            throw new NullReferenceException($"The review with id: {request.UserId} not found");

        return user;
    }
}