public interface IMovable : IMapEntity
{
    float Speed { get; } // make sure it gets updated somehow in the implementation IF needed (not constant speed)
}
