namespace Training.Data
{
    public class OutputFileInsertDto
    {
        public OutputFileInsertDto(byte[] bytes)
        {
            Bytes = bytes;
        }
        public byte[] Bytes { get; }
    }

}
