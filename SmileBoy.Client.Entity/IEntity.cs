﻿namespace SmileBoy.Client.Entity
{
    public interface IEntity<out TKey>
    {
        TKey id { get; }
    }
}
