﻿using Application.Contracts.Repositories.BaseRepository;
using Domain.Entities;

namespace Application.Contracts.Repositories;

public interface ITaskRepository : IGenericRepository<Tasks>;