using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class Text : Entry<string>
    {
        public Text()
        {
            AddValidation(new entry_validarions.NotEmpty());
        }
    }


}
