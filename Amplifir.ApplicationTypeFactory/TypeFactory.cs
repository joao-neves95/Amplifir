using System;
using Amplifir.Core.Interfaces;
using Amplifir.Core.Models;
using Amplifir.Core.DomainServices;
using Amplifir.Infrastructure.DataAccess;

namespace Amplifir.ApplicationTypeFactory
{
    /// <summary>
    /// 
    /// The application's Type factory.
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

                case ApplicationTypes.AuthenticationService:
                    return typeof( AuthenticationService );

                case ApplicationTypes.AppUserDapperStore:
                    return typeof( AppUserDapperStore );

                default:
                    throw new TypeAccessException( "Unknown type: " + Enum.GetName( typeof( ApplicationTypes ), applicationType ) );
            }
        }
    }
}
