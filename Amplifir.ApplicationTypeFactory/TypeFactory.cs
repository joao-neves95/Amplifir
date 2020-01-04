using System;
using Amplifir.Infrastructure.DataAccess;
using Amplifir.Infrastructure.DataAccess.Stores;

namespace Amplifir.ApplicationTypeFactory
{
    /// <summary>
    /// 
    /// The application's Type factory for accessing types that are not on the Core project (Infrastructure, etc).
    /// 
    /// </summary>
    public static class TypeFactory
    {
        public static Type Get( ApplicationTypes applicationType )
        {
            switch (applicationType)
            {
                case ApplicationTypes.DapperDBContext:
                    return typeof( DapperDBContext );

                case ApplicationTypes.AuditLogDapperStore:
                    return typeof( AuditLogDapperStore );

                case ApplicationTypes.AppUserDapperStore:
                    return typeof( AppUserDapperStore );

                default:
                    throw new TypeAccessException( "Unknown type: " + Enum.GetName( typeof( ApplicationTypes ), applicationType ) );
            }
        }
    }
}
