using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CredentialsSaver
{
    public class Credential
    {
        int Id { get; set; }    
        string Email { get; set; }
        string Password { get; set; }
        string NickName { get; set; }
        public Credential() { }
    }
}
