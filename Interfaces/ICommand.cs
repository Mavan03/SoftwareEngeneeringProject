using SoftwareEngeneeringProject.Entities;

namespace SoftwareEngeneeringProject.Interfaces
{
    public interface ICommand
    {
        public void Execute(Hero hero);
    }
}
