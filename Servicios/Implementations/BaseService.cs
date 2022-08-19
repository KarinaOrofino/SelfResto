using KO.Services.Interfaces;
using KO.Data.Interfaces;

namespace KO.Services.Implementations
{
    public class BaseService<TDatos>  : IBaseService
        where TDatos : IBaseData
    {
        protected readonly TDatos _datos;
        public BaseService(TDatos datos)
        {
            _datos = datos;
        }
    }
}
