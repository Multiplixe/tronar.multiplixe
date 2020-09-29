using multiplixe.ferramentas.comum.commands;

namespace multiplixe.ferramentas.comum.projects
{
    public class ProjectGRCP : Project
    {
        public ProjectGRCP(string @namespace, string name) : base(@namespace, name)
        {
            Type = typeOfProject.grcp;

        }

        public override Command CreatePublishCommand()
        {
            return new PublishGRPCCommand(this);
        }
    }
}
