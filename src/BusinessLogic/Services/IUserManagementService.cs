namespace BusinessLogic.Services
{
    public interface IUserManagementService : IUserService, IPersonaService, ISecurityPolicyService
    {
        // This interface now aggregates the smaller, more focused interfaces.
        // No method signatures are needed here directly.
    }
}
