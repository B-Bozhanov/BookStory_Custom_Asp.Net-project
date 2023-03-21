namespace HttpServer.Http.HttpCollections
{
    using System.Collections;

    public class HttpCollection : IEnumerable<Header>
    {
        private readonly Dictionary<string, Header> headers;

        public HttpCollection()
        {
            headers = new();
        }

        public int Count => headers.Count;

        public bool Contains(string key) => this.headers.ContainsKey(key);


        public void Add(Header header)
        {
            headers.Add(header.Name, header);
        }

        public void Add(string name, string value)
        {
            headers.Add(name, new Header(name, value));
        }

        public IEnumerator<Header> GetEnumerator()
            => headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
