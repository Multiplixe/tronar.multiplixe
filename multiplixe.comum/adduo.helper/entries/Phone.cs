using adduo.helper.extensionmethods;
using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class Phone : Format
    {
        public Phone() 
        {
            AddValidation(new entry_validarions.Phone());
        }

        public override string ToFormat()
        {
            return this.PhoneFormat();
        }
    }


}
