using adduo.helper.entries.entry_exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace adduo.helper.entries
{
    public abstract class BaseEntries
    {
        [JsonIgnore()]
        public List<Entry> Entries { get; private set; }

        public BaseEntries()
        {
            ResetEntry();
        }

        public void AddEntry(Entry entryDto)
        {
            Entries.Add(entryDto);
        }

        public void ResetEntry()
        {
            Entries = new List<Entry>();
        }

        /// <summary>
        /// Para indicar quais propriedades sofrerão validação.
        /// Utilizar o método AddPropertyToValidation.
        /// </summary>
        public abstract void SetEntries();

        /// <summary>
        /// Para adicionar validadores customizados às propriedades;
        /// Utilizar o método AddValidation de cada propriedade;
        /// </summary>
        public virtual void AddValidators() { }

        /// <summary>
        /// Para iniciar cada propriedade;
        /// Ex: Nome = new EntryName();
        /// </summary>
        public abstract void InitEntries();

        public void Validate()
        {
            SetEntries();
            AddValidators();

            Reset();

            foreach (var entry in this.Entries)
            {
                entry.Validate();
            }
        }

        public void Reset()
        {
            foreach (var entry in this.Entries)
            {
                entry.Reset();
            }
        }

        public bool AllAreValid()
        {
            return Entries.All(a => a.Status == STATUS.VALID);
        }

        public bool AnyIsValid()
        {
            return Entries.Any(a => a.Status == STATUS.VALID);
        }

        public bool AnyIsInvalid()
        {
            return Entries.Any(a => a.Status == STATUS.INVALID);
        }

    }
}
