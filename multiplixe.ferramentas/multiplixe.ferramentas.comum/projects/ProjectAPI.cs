using multiplixe.ferramentas.comum.commands;

namespace multiplixe.ferramentas.comum.projects
{
    public class ProjectAPI : Project
    {
        public ProjectAPI(string @namespace, string name) : base(@namespace, name)
        {
            Type = typeOfProject.api;
        }
 
        public override Command CreatePublishCommand()
        {
            return new PublishAPICommand(this);
        }
    }
}
