using adduo.helper.validations;

namespace adduo.helper.entries.entry_validations
{
    public class CPF : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = helper.validations.CPFValidation.Test(entry.Value);
                SetInvalidStatus(test);
            }
        }
    }
}
