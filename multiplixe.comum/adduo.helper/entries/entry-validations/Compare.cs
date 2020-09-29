namespace adduo.helper.entries.entry_validations
{
    public class Compare : BaseEntryValidation<string>, IEntryValidation<string>
    {
        private Entry<string> entryCompare { get; set; }

        public Compare(Entry<string> entry)
        {
            entryCompare = entry;
        }

        public void Validate()
        {
            if (CanValidate())
            {
                var test = entryCompare.Value.Equals(entry.Value);
                SetDifferentStatus(test, entryCompare);
                SetNoneStatus(test, entry);
            }
        }
    }
}
