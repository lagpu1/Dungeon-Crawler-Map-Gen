using System.Collections;
using System.Collections.Generic;
using RogueElements;

// EXAMPLES 1 - 4
public class TileTest : ITile
{
    public int ID { get; set; }

    public TileTest()
    {
        ID = 0;
    }
    public TileTest(int id)
    {
        ID = id;
    }

    protected TileTest(TileTest other)
    {
        this.ID = other.ID;
    }

    public bool TileEquivalent(ITile other)
    {
        TileTest tile = other as TileTest;
        if (tile == null)
            return false;
        return tile.ID == ID;
    }

    public ITile Copy() => new TileTest(this);

}
