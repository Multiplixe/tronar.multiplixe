using System;

namespace adduo.helper.extensionmethods
{
    public static class Guidextensionmethods
    {
        public static bool IsEmpty(this Guid _guid)
        {
            return _guid.Equals(Guid.Empty);
        }

        public static bool NotIsEmpty(this Guid _guid)
        {
            return !_guid.IsEmpty();
        }

        public static string ToStringNullIfEmpty(this Guid _guid)
        {
            return _guid.IsEmpty() ? string.Empty : _guid.ToString();
        }

    }
}
