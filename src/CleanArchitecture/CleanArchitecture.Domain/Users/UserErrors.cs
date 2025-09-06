using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Users;

public static class UserErrors
{

    public static Error NotFound = new(
        "User.NotFound",
        "No existe usuario con ese id"
    );
    public static Error InvalidCredencials = new(
       "User.InvalidCredencials",
       "Credenciales incorrectas"
   );
    
        public static Error AlreadyExists = new(
        "User.AlreadyExists",
        "Ya existe un usuario con ese email"
    );
    

}