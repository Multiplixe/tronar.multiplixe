using adduo.helper.extensionmethods;
using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    
    public class CPF : Format
    {
        public CPF() 
        {
            AddValidation(new entry_validarions.CPF());
        }

        public override string ToFormat()
        {
            return Value.CPFFormat();
        }
    }


}
