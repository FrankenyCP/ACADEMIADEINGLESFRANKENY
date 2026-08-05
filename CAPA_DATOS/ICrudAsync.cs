namespace CAPA_DATOS;

//TODO: Interfaz genérica de contrato CRUD asíncrono.
//TODO: Cualquier clase de acceso a datos (InstructorCD, NivelCD, etc.)
//TODO: que la implemente queda OBLIGADA a tener estas 4 operaciones en versión async.
public interface ICrudAsync<T>
{
    //TODO: Trae todos los registros de la tabla de forma asíncrona.
    Task<List<T>> ObtenerTodosAsync();

    //TODO: Inserta un nuevo registro de forma asíncrona. Devuelve true si se insertó.
    Task<bool> InsertarAsync(T entidad);

    //TODO: Edita/actualiza un registro existente de forma asíncrona. Devuelve true si se actualizó.
    Task<bool> ActualizarAsync(T entidad);

    //TODO: Elimina un registro por su Id de forma asíncrona. Devuelve true si se eliminó.
    Task<bool> EliminarAsync(int id);
}
