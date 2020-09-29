using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class String128 : String
    {
        public String128()  
        {
            AddValidation(new entry_validarions.MaxLength(128));
        }
    }


}
