using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class String64 : String
    {
        public String64() 
        {
            AddValidation(new entry_validarions.MaxLength(64));
        }
    }


}
