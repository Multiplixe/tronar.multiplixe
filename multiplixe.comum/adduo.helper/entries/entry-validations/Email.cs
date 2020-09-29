using adduo.helper.validations;

namespace adduo.helper.entries.entry_validations
{
    public class Email : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = helper.validations.EmailValidation.Test(entry.Value);
                SetInvalidStatus(test);
            }
        }
    }
}
