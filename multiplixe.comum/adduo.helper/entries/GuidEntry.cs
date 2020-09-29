using System;

namespace adduo.helper.entries
{
    public class GuidEntry : Entry<Guid>
    {
        public GuidEntry(bool newGuid)
        {
            Value = Guid.Empty;

            if (newGuid)
            {
                Value = Guid.NewGuid();
            }
        }

        public GuidEntry()
        {
            Value = Guid.Empty;
        }
    }


}
