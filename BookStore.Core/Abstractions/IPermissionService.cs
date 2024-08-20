using BookStore.Core.Enums;

namespace BookStore.Application.Services
{
    public interface IPermissionService
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid UserId);
    }
}