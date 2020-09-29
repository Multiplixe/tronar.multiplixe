using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class String32 : String
    {
        public String32()  
        {
            AddValidation(new entry_validarions.MaxLength(32));
        }
    }


}
