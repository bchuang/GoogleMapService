namespace Configuration
{
    public interface ISettings
    {
        string this[string name]
        {
            get;
        }
    }
}