using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public  class PublishAPICommand : Command
    {
        public PublishAPICommand(Project project) : base(project)
        {
        }

        public override string ToString()
        {
            return $"/c dotnet build {project.FileProjectPath} -c Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile";
        }

    }
}
