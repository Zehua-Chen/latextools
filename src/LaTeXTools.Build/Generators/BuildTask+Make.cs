using LaTeXTools.Build.Tasks;

namespace LaTeXTools.Build.Generators
{
    public static class BuildTaskMake
    {
        public static Makefile GetMakefile(this BuildTask buildTask)
        {
            return new Makefile();
        }
    }
}
