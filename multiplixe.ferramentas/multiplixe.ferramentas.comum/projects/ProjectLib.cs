using multiplixe.ferramentas.comum.commands;

namespace multiplixe.ferramentas.comum.projects
{
    public class ProjectLib : Project
    {
        public ProjectLib() : base()
        {

        }

        public ProjectLib(string @namespace, string name) : base(@namespace, name)
        {
            Type = typeOfProject.lib;
        }

        public override Command CreatePublishCommand()
        {
            return new PublishLibCommand(this);
        }
    }
}
