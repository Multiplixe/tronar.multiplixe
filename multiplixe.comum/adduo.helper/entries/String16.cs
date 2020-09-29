using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class String16 : String
    {
        public String16() 
        {
            AddValidation(new entry_validarions.MaxLength(16));
        }
    }


}
