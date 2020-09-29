namespace adduo.helper.entries.entry_validations
{
    public class BaseEntryValidation<T>
    {
        public IEntry<T> entry { get; set; }

        public void Set(IEntry<T> entry)
        {
            this.entry = entry;
        }

        protected bool CanValidate()
        {
            return entry.Status != STATUS.INVALID;
        }

        protected void SetAlreadyStatus(bool valid)
        {
            SetStatus(valid, CODE.ALREADY, entry);
        }

        protected void SetEmptyStatus(bool valid)
        {
            SetStatus(valid, CODE.EMPTY, entry);
        }

        protected void SetInvalidStatus(bool valid)
        {
            SetStatus(valid, CODE.INVALID, entry);
        }

        protected void SetInactiveStatus(bool valid)
        {
            SetStatus(valid, CODE.INACTIVE, entry);
        }

        protected void SetErrorStatus(bool valid)
        {
            SetStatus(valid, CODE.ERROR, entry);
        }

        protected void SetNotFoundStatus(bool valid)
        {
            SetStatus(valid, CODE.NOTFOUND, entry);
        }

        protected void SetDifferentStatus(bool valid)
        {
            SetStatus(valid, CODE.DIFFERENT, entry);
        }

        protected void SetDifferentStatus(bool valid, Entry<T> entry)
        {
            SetStatus(valid, CODE.DIFFERENT, entry);
        }

        protected void SetMaxlengthStatus(bool valid)
        {
            SetStatus(valid, CODE.MAXLENGTH, entry);
        }

        protected void SetNoneStatus(bool valid)
        {
            SetStatus(valid, CODE.NONE, entry);
        }

        protected void SetNoneStatus(bool valid, IEntry<T> entry)
        {
            SetStatus(valid, CODE.NONE, entry);
        }

        private void SetStatus(bool valid, CODE error, IEntry<T> targetProperty)
        {
            targetProperty.Status = valid ? STATUS.VALID : STATUS.INVALID;

            if (!valid)
            {
                targetProperty.Code = error;
            }
        }
    }

}
