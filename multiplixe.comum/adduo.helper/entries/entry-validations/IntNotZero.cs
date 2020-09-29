using adduo.helper.extensionmethods;

namespace adduo.helper.entries.entry_validations
{
    public class IntNotZero : BaseEntryValidation<int>, IEntryValidation<int>
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
