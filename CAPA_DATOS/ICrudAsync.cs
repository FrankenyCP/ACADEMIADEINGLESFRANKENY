namespace CAPA_DATOS;

// TODO: Interfaces Y Asincrónicos - Interfaz genérica que define el contrato CRUD asíncrono para las clases de acceso a datos
// TODO: Arquitectura en Capas - Interfaz perteneciente a CAPA_DATOS, establece el contrato que deben cumplir las clases CD (AlumnoCD, InstructorCD, NivelCD, etc.)
// Cualquier clase de acceso a datos (InstructorCD, NivelCD, etc.)
// que la implemente queda OBLIGADA a tener estas 4 operaciones en versión async.
public interface ICrudAsync<T>
{
    // TODO: Interfaces Y Asincrónicos - Firma del método asíncrono que trae todos los registros de la tabla
    Task<List<T>> ObtenerTodosAsync();

    // TODO: Interfaces Y Asincrónicos - Firma del método asíncrono que inserta un nuevo registro, retorna true si la operación fue exitosa
    Task<bool> InsertarAsync(T entidad);

    // TODO: Interfaces Y Asincrónicos - Firma del método asíncrono que edita/actualiza un registro existente, retorna true si la operación fue exitosa
    Task<bool> EditarAsync(T entidad);

    // TODO: Interfaces Y Asincrónicos - Firma del método asíncrono que elimina un registro por su Id, retorna true si la operación fue exitosa
    Task<bool> EliminarAsync(int id);
}
