using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class DecimalNumber : Entry<decimal>
    {
        public DecimalNumber()
        {
            AddValidation(new entry_validarions.DecimalNotZero());
        }
    }


}
