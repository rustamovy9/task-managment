using Application.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;

namespace Infrastructure.ImplementationContract.Repositories;

public class TaskHistoryRepository(DataContext dbContext)
    : GenericRepository<TaskHistory>(dbContext), ITaskHistoryRepository;