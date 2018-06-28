using System.Collections.Generic;

namespace Stacks.API.ViewModels
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseViewModel
    {
        public ICollection<StackListItemViewModel> Stacks { get; set; }
        public SessionInfoViewModel SessionInfo { get; set; }
    }

    public class SessionInfoViewModel
    {
        public string IdToken { get; set; }
        public string Username { get; set; }
    }
}
