using multiplixe.ferramentas.comum.projects;

namespace multiplixe.ferramentas.comum.commands
{
    public  class Command
    {
        public string Name { get; }
        protected Project project { get; }

        public Command(Project project)
        {
            this.project = project;
            Name = project.FullName;
        }
    }
}

