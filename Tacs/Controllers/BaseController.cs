using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Tacs.Controllers
{
    public class BaseController : Controller
    {
        public void RegistraErros(DbEntityValidationException e)
        {
            foreach (var eve in e.EntityValidationErrors)
            {
                foreach (var ve in eve.ValidationErrors)
                {
                    ModelState.AddModelError(ve.PropertyName, ve.ErrorMessage);
                }
            }
        }
    }
}
