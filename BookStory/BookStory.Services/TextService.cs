namespace BookStory.Services
{
    using Newtonsoft.Json;

    using BookStory.Services.TextDto;

    public class TextService : ITextService
    {
        public IEnumerable<ExportTexModel> Deserialize(string text)
        {
            var model = JsonConvert.DeserializeObject<IEnumerable<ExportTexModel>>(text);

            return model!;
        }

        public string Serialize(ImportTextModel model)
        {
            var modelStr = JsonConvert.SerializeObject(model);

            return modelStr;
        }
    }
}
