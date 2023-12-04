﻿namespace HotelApi.Models
{
    public interface IConverter<T,U>
    {
        T Convert(U model);
        U Convert(T model);

    }
}
