namespace HttpServer.Http
{
    public class Header
    {
        public Header(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public Header(string line)
        {
            var headerParts = line.Split(": ", 2);

            this.Name = headerParts[0];
            this.Value = headerParts[1];
        }

        public string Name { get; init; }

        public string Value { get; init; }

        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }
}
