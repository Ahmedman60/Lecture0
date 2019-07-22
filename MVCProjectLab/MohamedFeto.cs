using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProjectLab
{
    public  class MohamedFeto
    {
        public MohamedFeto Feto=new MohamedFeto();
        private MohamedFeto()
        {
            
        }
        public MohamedFeto GetFeto()
        {
           
            
            return Feto;
        }

        public static bool CompareFeto<T>(T obj1, T obj2, Action Method)
        {
            Method.Invoke();
            //can't put == this may be a diffrence between those two 
            // include  the == compare references and equals compare the content remmember you have to redefine the equal in custom classes 
            //that uses this method.
            if (obj1.Equals(obj2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}