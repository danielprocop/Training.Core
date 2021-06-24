namespace SystemWrapper.Interface
{
    public interface IFileInfo
    {
        string FullName { get; }
        string Name { get; }
        string Extension { get; }
    }
}