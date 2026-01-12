using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace RepositoryContracts;

public interface IFacultyRepository
{
    public Task<List<Faculty>> GetFaculties();
    public Task<Faculty> GetFaculty(string facultyId);
}
