using Ecommerce.BuildingBlocks.Application;

namespace Ecommerce.API.Configuration.ExecutionContext;

public class ExecutionContextAccessor : IExecutionContextAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public ExecutionContextAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid UserId 
    { 
       get
        {
            if (_contextAccessor
                .HttpContext?    
                .User?
                .Claims?
                .SingleOrDefault(x => x.Type == "sub")
                .Value != null)
            {
                return Guid.Parse(_contextAccessor
                    .HttpContext
                    .User
                    .Claims
                    .SingleOrDefault(x => x.Type == "sub").Value);
            }

            throw new ApplicationException("User context is not available");
        }
}
}
