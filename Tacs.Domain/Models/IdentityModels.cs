using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Tacs.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tacs.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Matricula { get; set; }
        //public TipoUsuario TipoUsuario { get; set; }
        public virtual ICollection<Disciplina> Disciplinas { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            
            return userIdentity;
        }

        public bool isInRole(String roleName)
        {
            TacsContext db = new TacsContext();
            var role = db.Roles.Where(m => m.Name == roleName).FirstOrDefault();
            
            var roles = this.Roles.Count;
            if(role != null)
            {
                if (this.Roles.Where(m => m.RoleId == role.Id).Count() > 0)
                {
                    return true;
                }
            }

            return false;

        }
    }

}