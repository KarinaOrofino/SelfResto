using KO.Data.Interfaces;
using KO.Services.Interfaces;

namespace KO.Services.Implementations
{
    public class BaseService<TDatos> : IBaseService
        where TDatos : IBaseData
    {
        protected readonly TDatos _datos;
        public BaseService(TDatos datos)
        {
            _datos = datos;
        }
    }
}
