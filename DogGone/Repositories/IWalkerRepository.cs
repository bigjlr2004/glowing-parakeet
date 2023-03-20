using DogGone.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGone.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);
    }
}
