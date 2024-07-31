using AlikAndFlorasWedding.Models;

namespace AlikAndFlorasWedding.Services.ApplicationService;

public interface IApplicationService
{
    Task SaveApplicationAsync(ApplicationModel application);
}