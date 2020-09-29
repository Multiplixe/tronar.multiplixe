using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public  class AddNugetCommand : Command
    {
        public AddNugetCommand(Project project) : base(project)
        {
                Log.Write($"- adicionando pacote no Nuget;");
        }

        public override string ToString()
        {
            var fileNuget = $"{project.FullName}.{project.NewVersion}.nupkg";

            return $"/c nuget add -source {project.NugetSource} {fileNuget}";
        }
    }
}
