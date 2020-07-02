namespace LaTeXTools.Project
{
    public class LaTeXBackend
    {
        public string OutputDirectory { get; set; }
        public string Entry { get; set; }

        public virtual string Arguments => $"-output-directory={this.OutputDirectory} {this.Entry}";

        private LaTeXBackend()
        {
        }

        public static LaTeXBackend Create(string backend)
        {
            return new LaTeXBackend();
        }
    }
}
