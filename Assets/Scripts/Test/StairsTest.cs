using System.Collections;
using System.Collections.Generic;
using RogueElements;

public class StairsUp : Stairs, IEntrance
{
    public StairsUp()
        : base()
    {
    }

    protected StairsUp(StairsUp other)
        : base(other)
    {
    }


    public override ISpawnable Copy() { return new StairsUp(this); }
}

public class StairsDown : Stairs, IExit
{
    public StairsDown()
        : base()
    {
    }

    protected StairsDown(StairsDown other)
        : base(other)
    {
    }

    public override ISpawnable Copy() { return new StairsDown(this); }
}

public abstract class Stairs
{
    protected Stairs()
    {
    }


    protected Stairs(Stairs other)
    {
        this.Loc = other.Loc;
    }

    public Loc Loc { get; set; }

    public abstract ISpawnable Copy();
}
