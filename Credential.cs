using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_CredentialsSaver;

public class Credential
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string NickName { get; set; }
    public Credential() { }
}
