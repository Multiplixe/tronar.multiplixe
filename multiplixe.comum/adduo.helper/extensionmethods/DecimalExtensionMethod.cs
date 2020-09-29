namespace adduo.helper.extensionmethods
{
    public static class DecimalExtensionMethod
    {
        public static bool Zero(this decimal number)
        {
            return number.Equals(0);
        }

        public static bool NotZero(this decimal number)
        {
            return !number.Equals(0);
        }


    }
}
