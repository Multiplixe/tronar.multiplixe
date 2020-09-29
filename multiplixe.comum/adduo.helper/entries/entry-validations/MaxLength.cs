namespace adduo.helper.entries.entry_validations
{
    public class MaxLength : BaseEntryValidation<string>, IEntryValidation<string>
    {
        private int maxLength { get; }

        public MaxLength(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public void Validate()
        {
            if (CanValidate() && 
                entry.Value != null && 
                !maxLength.Equals(0))
            {
                var test = entry.Value.Length <= maxLength;

                SetMaxlengthStatus(test);
            }
        }
    }
}
