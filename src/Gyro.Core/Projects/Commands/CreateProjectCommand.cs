using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gyro.Core.Entities;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using Gyro.Core.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Core.Projects.Commands
{
    public sealed record CreateProjectRequest(string Name, string Description) : IRequest<CreateProjectResponse>;

    public sealed record CreateProjectResponse;

    public sealed class CreateProjectCommand : IRequestHandler<CreateProjectRequest, CreateProjectResponse>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IGyroContext _db;

        public CreateProjectCommand(ICurrentUserService currentUserService, IGyroContext db)
        {
            _currentUserService = currentUserService;
            _db = db;
        }

        public async Task<CreateProjectResponse> Handle(CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            // Project names are unique per user, meaning multiple users may create a project with the same name
            var userId = int.Parse(_currentUserService.UserId);
            var project = await _db.Projects
                .Where(p => p.Lead.Id == userId)
                .Where(p => p.Name == request.Name)
                .FirstOrDefaultAsync(cancellationToken);

            if (project is not null)
            {
                throw new GyroException("A project with that name already exists");
            }

            project = new Project(request.Name, userId)
            {
                Description = request.Description
            };
            
            _db.Projects.Add(project);
            await _db.SaveAsync(cancellationToken);

            return new CreateProjectResponse();
        }
    }
}