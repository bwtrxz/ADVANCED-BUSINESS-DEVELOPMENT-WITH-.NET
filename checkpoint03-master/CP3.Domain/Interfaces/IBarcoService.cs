using CP3.Domain.Entities;

namespace CP3.Domain.Interfaces
{
    public interface IBarcoService
    {
        Task<BarcoEntity?> ObterBarcoPorIdAsync(int id);
    }
}
