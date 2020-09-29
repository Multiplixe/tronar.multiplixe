using adduo.helper.extensionmethods;

namespace adduo.helper.entries.entry_validations
{
    public class NotEmpty : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = entry.Value.NotIsNullOrEmpty();

                SetEmptyStatus(test);
            }
        }
    }
}
