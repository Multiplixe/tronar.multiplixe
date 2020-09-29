using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class Email : String128
    {
        public Email()  
        {
            AddValidation(new entry_validarions.Email());

        }
    }


}
