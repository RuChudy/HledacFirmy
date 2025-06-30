using System.Threading.Tasks;

namespace HledacFirmy.Data;

public interface IHledacFirmyDbSchemaMigrator
{
    Task MigrateAsync();
}
