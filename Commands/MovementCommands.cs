using SoftwareEngeneeringProject.Entities;
using SoftwareEngeneeringProject.Interfaces;


namespace SoftwareEngeneeringProject.Commands
{
    public class MoveLeftCommand : ICommand
    {
        public void Execute(Hero hero)
        {
            hero.MoveLeft();
        }
    }

    public class MoveRightCommand : ICommand
    {
        public void Execute(Hero hero)
        {
            hero.MoveRight();
        }
    }

    public class JumpCommand : ICommand
    {
        public void Execute(Hero hero)
        {
            hero.Jump();
        }
    }

    public class CrouchCommand : ICommand
    {
        public void Execute(Hero hero)
        {
            hero.Crouch();
        }
    }
}
