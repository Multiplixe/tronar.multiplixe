using adduo.helper.extensionmethods;

namespace adduo.helper.entries.entry_validations
{
    public class SQLInjection : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = !entry.Value.SQLInjection();

                SetInvalidStatus(test);
            }
        }
    }
}
