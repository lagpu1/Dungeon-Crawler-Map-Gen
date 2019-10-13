using System.Collections;
using System.Collections.Generic;
using RogueElements;

public class Mob : ISpawnable
{
    public int ID { get; set; }
    public Loc Loc { get; set; }

    public Mob() { }
    public Mob(int id) { ID = id; }
    public Mob(int id, Loc loc) { ID = id; Loc = loc; }
    protected Mob(Mob other)
        : this(other.ID, other.Loc)
    {
    }


    public ISpawnable Copy() => new Mob(this);
}
