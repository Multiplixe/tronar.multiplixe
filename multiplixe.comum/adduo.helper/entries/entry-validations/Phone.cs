using adduo.helper.validations;

namespace adduo.helper.entries.entry_validations
{
    public class Phone : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = PhoneValidation.Test(entry.Value);
                SetInvalidStatus(test);
            }
        }
    }
}
