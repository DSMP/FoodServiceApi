using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodServiceApi.Model.Services
{
    public interface ICrudService<T>
    {
        List<T> GetAll();

    }
}
