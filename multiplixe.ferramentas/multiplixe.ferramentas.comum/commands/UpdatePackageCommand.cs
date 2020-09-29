using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public  class UpdatePackageCommand : Command
    {
        private string packageName { get; }

        public UpdatePackageCommand(Project project, string packageName) : base(project)
        {
            this.packageName = packageName;
        }

        public override string ToString()
        {
            return $"/c dotnet add {project.FileProjectPath} package {packageName} -s {project.NugetSource}";
        }
    }
}

