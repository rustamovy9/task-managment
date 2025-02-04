using Application.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;

namespace Infrastructure.ImplementationContract.Repositories;

public class TaskRepository(DataContext dbContext)
    : GenericRepository<Tasks>(dbContext), ITaskRepository;