using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class Name : String128
    {
        public Name()  
        {
            AddValidation(new entry_validarions.OnlyText());

        }
    }
}
