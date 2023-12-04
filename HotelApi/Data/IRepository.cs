﻿namespace HotelApi.Data
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
