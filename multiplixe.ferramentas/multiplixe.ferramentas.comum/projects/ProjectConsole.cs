using multiplixe.ferramentas.comum.commands;

namespace multiplixe.ferramentas.comum.projects
{
    public class ProjectConsole : Project
    {
        public ProjectConsole(string @namespace, string name) : base(@namespace, name)
        {
            Type = typeOfProject.console;

        }

        public override Command CreatePublishCommand()
        {
            return new PublishConsoleCommand(this);
        }
    }
}
