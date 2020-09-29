namespace adduo.helper.extensionmethods
{
    public static class Intextensionmethods
    {
        public static bool Zero(this int _int)
        {
            return _int.Equals(0);
        }

        public static bool NotZero(this int _int)
        {
            return !_int.Equals(0);
        }
    }
}
