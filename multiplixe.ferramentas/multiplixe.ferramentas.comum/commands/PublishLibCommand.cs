using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public  class PublishLibCommand : Command
    {
        public PublishLibCommand(Project project) : base(project)
        {
            Log.Write($"- criando pacote;");
        }


        public override string ToString()
        {
            return $"/c dotnet pack {project.FileProjectPath} --output";
        }
    }
}

