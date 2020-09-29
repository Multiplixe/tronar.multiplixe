namespace adduo.helper.entries
{
    public interface IEntry<T>
    {
        T Value { get; set;}
        STATUS Status { get; set; }
        CODE Code { get; set; }
    }
}
