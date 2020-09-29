using adduo.helper.extensionmethods;

namespace adduo.helper.entries.entry_validations
{
    public class Password : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var entryPassword = (entries.Password)entry;
                var test = entryPassword.Value.NotIsNullOrEmpty();
                SetEmptyStatus(test);
            }
        }
    }
}
