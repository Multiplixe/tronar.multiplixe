using entry_validarions = adduo.helper.entries.entry_validations;

namespace adduo.helper.entries
{
    public class String : Entry<string>
    {
        public String()
        {
            AddValidations();
        }

        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(this.Value);
        }

        public string ValueEmptyIfNull()
        {
            return string.IsNullOrEmpty(this.Value) ? string.Empty : this.Value;
        }


        private void AddValidations()
        {
            AddValidation(new entry_validarions.NotEmpty());
            AddValidation(new entry_validarions.SQLInjection());
        }
    }


}
