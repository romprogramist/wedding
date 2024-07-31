using AlikAndFlorasWedding.Data;
using AlikAndFlorasWedding.Models;

namespace AlikAndFlorasWedding.Services.ApplicationService;

public class ApplicationService : IApplicationService
{
    private readonly DataContext _context;

    public ApplicationService(DataContext context)
    {
        _context = context;
    }
    
    public async Task SaveApplicationAsync(ApplicationModel application)
    {
        await _context.ClientApplications.AddAsync(new ClientApplication
        {
            CreationDateTime = DateTime.Now.ToUniversalTime(),
            Name = application.Name,
            Phone = application.Phone,
            SitePage = application.SitePage,
            AdditionalInfo = application.AdditionalInfo,
            UtmInfo = application.UtmInfo,
        });

        await _context.SaveChangesAsync();
    }
}