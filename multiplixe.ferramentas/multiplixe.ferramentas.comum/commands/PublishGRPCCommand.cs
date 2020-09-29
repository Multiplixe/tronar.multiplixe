using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public  class PublishGRPCCommand : Command
    {
        public PublishGRPCCommand(Project project) : base(project)
        {
                Log.Write($"- publicando projeto GRPC;");
        }

        public override string ToString()
        {
            return $"/c dotnet build {project.FileProjectPath} -c Release /p:DeployOnBuild=true /p:PublishProfile=FolderProfile";
        }

    }
}
