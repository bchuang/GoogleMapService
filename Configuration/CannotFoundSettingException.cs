using System;

namespace Configuration
{
    public sealed class CannotFoundSettingException : Exception
    {
        public CannotFoundSettingException(string name)
            : base(string.Format("No '{0}' Value。", name))
        {
        }
    }
}
