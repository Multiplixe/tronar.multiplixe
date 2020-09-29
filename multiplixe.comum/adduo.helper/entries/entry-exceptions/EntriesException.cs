using adduo.helper.envelopes;
using System;

namespace adduo.helper.entries.entry_exceptions
{
    public class EntriesException<T> : Exception
    {
        public ResponseEnvelope<T> ResponseEnvelope { get; }

        public EntriesException(ResponseEnvelope<T> response)
        {
            ResponseEnvelope = response;
        }

    }
}
