
using adduo.helper.extensionmethods;

namespace adduo.helper.entries.entry_validations
{
    public class DecimalNotZero : BaseEntryValidation<decimal>, IEntryValidation<decimal>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = entry.Value.NotZero();

                SetEmptyStatus(test);
            }
        }
    }
}
