using ConsoleAppSWAPI.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppSWAPI
{
    public class InputValidate
    {
        public static bool mgltTypeValidate(string mglt)
        {
            bool valid = false;

            if (long.TryParse(mglt, out long l) && Convert.ToInt64(mglt) >= 0 )
            {
                valid = true;
            }

            return valid;
        }
    }
}
