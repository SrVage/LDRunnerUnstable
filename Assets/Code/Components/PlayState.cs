using Leopotam.Ecs;

namespace Client.Components
{
    public enum PlayStates
    {
        Menu = 0,
        EnterLevel = 1,
        Play = 2
    }
    public struct PlayState
    {
        public PlayStates PlayStates;
    }
}