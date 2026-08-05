using System;
using System.Collections.Generic;
using System.Text;

namespace CAPA_DATOS;

// Interfaz genérica para operaciones CRUD asíncronas.
// Cualquier clase "CD" (AlumnoCD, InstructorCD, etc.) puede implementarla
// reemplazando T por la entidad que maneje.
public interface ICrudAsync<T>
{
    Task<bool> InsertarAsync(T entidad);
    Task<bool> ActualizarAsync(T entidad);
    Task<bool> EliminarAsync(int id);
    Task<List<T>> ObtenerTodosAsync();
}