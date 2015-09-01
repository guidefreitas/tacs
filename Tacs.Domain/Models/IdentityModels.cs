using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tacs.Domain.Models;
using System;
using System.Collections.Generic;

namespace Tacs.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Matricula { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public virtual ICollection<Disciplina> Disciplinas { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }
    }

}