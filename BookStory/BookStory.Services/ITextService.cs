namespace BookStory.Services
{
    using BookStory.Services.TextDto;

    public interface ITextService
    {
        public IEnumerable<ExportTexModel> Deserialize(string text);

        public string Serialize(ImportTextModel model);
    }
}
