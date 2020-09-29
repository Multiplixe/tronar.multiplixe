namespace adduo.helper.entries
{
    public interface IEntryValidation<T>
    {
        void Set(IEntry<T> entry);
        void Validate();
    }
}
