using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class IntEntry : Entry<int>
    {
        public IntEntry()
        {
            AddValidation(new entry_validarions.IntNotZero());
        }
    }


}
