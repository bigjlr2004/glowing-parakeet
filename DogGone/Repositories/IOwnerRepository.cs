using DogGone.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGone.Repositories
{
    public interface IOwnerRepository
    {
        List<Owner> GetAllOwners();
        Owner GetOWnerById(int id);
        
    }
}
