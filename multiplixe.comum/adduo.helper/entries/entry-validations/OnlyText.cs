﻿using adduo.helper.validations;

namespace adduo.helper.entries.entry_validations
{
    public class OnlyText : BaseEntryValidation<string>, IEntryValidation<string>
    {
        public void Validate()
        {
            if (CanValidate())
            {
                var test = StringValidation.Test(entry.Value);
                SetInvalidStatus(test);
            }
        }
    }
}
