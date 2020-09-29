using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace adduo.helper.entries
{
    public class Entry  
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public STATUS Status { get; set; }

        [JsonPropertyName("code")]
        public CODE Code { get; set; }

        public bool Edit { get; set; }

        public bool Prop { get { return true; } }

        public Entry()
        {
            Reset();
        }

        public void Reset()
        {
            Status = STATUS.NONE;
            Code = CODE.NONE;
        }

        public bool IsValidStatus()
        {
            return Status == STATUS.VALID;
        }

        public bool IsInvalidStatus()
        {
            return Status == STATUS.INVALID;
        }

        public bool IsInvalidOrEmptyStatus()
        {
            return Status == STATUS.INVALID || Status == STATUS.NONE;
        }

        public bool IsEmptyStatus()
        {
            return Status == STATUS.NONE;
        }

        public bool IsNoneCode()
        {
            return Code == CODE.NONE;
        }

        public bool IsAlreadyCode()
        {
            return Code == CODE.ALREADY;
        }

        public bool IsEmptyCode()
        {
            return Code == CODE.EMPTY;
        }

        public bool IsInvalidCode()
        {
            return Code == CODE.INVALID;
        }

        public bool IsInactiveCode()
        {
            return Code == CODE.INACTIVE;
        }

        public bool IsErrorCode()
        {
            return Code == CODE.ERROR;
        }

        public bool IsNotFoundCode()
        {
            return Code == CODE.NOTFOUND;
        }

        public bool IsDifferentCode()
        {
            return Code == CODE.DIFFERENT;
        }

        public bool IsMaxLengthCode()
        {
            return Code == CODE.MAXLENGTH;
        }

        public virtual bool Validate() { return true; }

    }

    public class Entry<T> : Entry, IEntry<T>
    {
        [JsonPropertyName("value")]
        public T Value { get; set; }

        [JsonPropertyName("defaultValue")]
        public T DefaultValue { get; set; }

        private List<IEntryValidation<T>> Validations { get; set; }

        public Entry()
        {
            Validations = new List<IEntryValidation<T>>();
        }

        public void AddValidation(IEntryValidation<T> validation)
        {
            validation.Set(this);

            Validations.Add(validation);
        }

        public override bool Validate()
        {
            foreach (var validation in Validations)
            {
                validation.Validate();
            }

            return true;
        }
    }


}
