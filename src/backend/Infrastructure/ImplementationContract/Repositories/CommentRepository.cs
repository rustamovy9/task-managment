using Application.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.DataAccess;
using Infrastructure.ImplementationContract.Repositories.BaseRepository;

namespace Infrastructure.ImplementationContract.Repositories;

public class CommentRepository(DataContext dbContext)
    : GenericRepository<Comment>(dbContext), ICommentRepository;