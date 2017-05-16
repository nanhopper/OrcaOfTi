using System;

public class Map
{    
    public const int Size = 30;
    private readonly Random Random = new Random();

    private readonly View View;
    private readonly Entity[][] Matrix;

    public Map(View view)
    {
        View = view;
        Matrix = new Entity[Size][];

        for (int i = 0; i < Size; i++)
        {
            Matrix[i] = new Entity[Size];
        }
    }

    public void Add(Entity entity)
    {
        Matrix[entity.Row][entity.Column] = entity;
        View.DisplayEntity(entity);
    }

    public void Remove(Entity entity)
    {
        Matrix[entity.Row][entity.Column] = null;
        View.DisplayEntity(entity, true);
    }

    public bool TrySpawnAt<T>(int row, int column, out T entity) where T : Entity, new()
    {
        entity = null;
        if (IsEmpty(row, column))
        {
            entity = new T();
            entity.MoveTo(row, column);
            Add(entity);
            return true;
        }
        return false;
    }
    public T Spawn<T>() where T : Entity, new()
    {
        T entity = null;
        int row, column;
        do
        {
            column = Random.Next(Size);
            row = Random.Next(Size);
        }
        while(!TrySpawnAt(row, column, out entity));

        return entity;
    }

    public bool TryGetNeighbor<T>(Entity entity, out T neighbor) where T : Entity
    {
        neighbor = LeftNeighbor(entity) as T
            ?? RightNeighbor(entity) as T
            ?? TopNeighbor(entity) as T
            ?? BottomNeighbor(entity) as T;
        return neighbor != null;
    }
    
    public Entity LeftNeighbor(Entity entity)
    {
        return (entity.Column == 0) ? null : Matrix[entity.Row][entity.Column -1];
    }

    public Entity RightNeighbor(Entity entity)
    {
        return (entity.Column == Matrix[0].Length-1) ? null : Matrix[entity.Row][entity.Column +1];
    }

    public Entity TopNeighbor(Entity entity)
    {
        return (entity.Row == 0) ? null : Matrix[entity.Row-1][entity.Column];
    }

    public Entity BottomNeighbor(Entity entity)
    {
         return (entity.Row == Matrix.Length -1) ? null : Matrix[entity.Row+1][entity.Column];
    }

    public bool IsEmpty(int row, int column)
    {
        return Matrix[row][column] == null;
    }
}