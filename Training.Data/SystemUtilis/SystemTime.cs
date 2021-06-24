using System;

namespace Training.Data
{
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }

}
