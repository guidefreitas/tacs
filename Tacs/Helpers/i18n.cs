using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacs.Resources;

namespace Tacs.Helpers
{
    public static class i18n
    {
        
        public static String T(String path)
        {

            String value = Resource.ResourceManager.GetString(path);

            if (value != null)
            {
                return value;
            }
            else
            {
                return "NT-" + path;
            }
        }
    }
}
