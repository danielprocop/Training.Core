using System;
using System.Security.Principal;
using System.Text;

namespace Training.Data
{
    public static class SystemIdentity
    {
        public static Func<string> CurrentName = () => WindowsIdentity.GetCurrent()?.Name;
    }

}
