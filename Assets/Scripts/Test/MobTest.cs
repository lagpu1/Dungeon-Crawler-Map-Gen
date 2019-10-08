using System.Collections;
using System.Collections.Generic;
using RogueElements;

public class Mob
{
    public int ID { get; set; }
    public Loc Loc { get; set; }

    public Mob() { }
    public Mob(int id) { ID = id; }
    public Mob(int id, Loc loc) { ID = id; Loc = loc; }
}
