using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

// TODO: Modelo que representa la tabla MATRICULAS en la base de datos
// Contiene campos extra para mostrar información relacionada en la grilla
public class _Matricula
{
    private int idMatricula;
    private int idAlumno;
    private int idNivel;
    private int idInstructor;
    private DateTime fechaMatricula;

    public int IdMatricula { get => idMatricula; set => idMatricula = value; }
    public int IdAlumno { get => idAlumno; set => idAlumno = value; }
    public int IdNivel { get => idNivel; set => idNivel = value; }
    public int IdInstructor { get => idInstructor; set => idInstructor = value; }
    public DateTime FechaMatricula { get => fechaMatricula; set => fechaMatricula = value; }

    // TODO: Campos extra para mostrar información relacionada en la grilla
    public string NombreAlumno { get; set; } = string.Empty;
    public string NombreNivel { get; set; } = string.Empty;
    public string NombreInstructor { get; set; } = string.Empty;

    // TODO: Constructor vacío — inicializa la fecha con el día de hoy
    public _Matricula()
    {
        this.fechaMatricula = DateTime.Today;
    }

    // TODO: Constructor parametrizado — inicializa todos los campos de la matrícula
    public _Matricula(int idMatricula, int idAlumno, int idNivel,
                      int idInstructor, DateTime fechaMatricula)
    {
        this.idMatricula = idMatricula;
        this.idAlumno = idAlumno;
        this.idNivel = idNivel;
        this.idInstructor = idInstructor;
        this.fechaMatricula = fechaMatricula;
    }
}

// TODO: DAL de Matrículas — maneja todas las operaciones SQL sobre la tabla MATRICULAS
// Respeta la arquitectura en capas: solo accede a datos, sin lógica de negocio
public class MatriculaCD
{
    // TODO: Inserta una nueva matrícula en la base de datos
    // Retorna true si la inserción fue exitosa, false si falló
    public bool Insertar(_Matricula m)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"INSERT INTO MATRICULAS (IDALUMNO, IDNIVEL, IDINSTRUCTOR, FECHAMATRICULA)
              VALUES (@alumno, @nivel, @instructor, @fecha)", con))
        {
            cmd.Parameters.AddWithValue("@alumno", m.IdAlumno);
            cmd.Parameters.AddWithValue("@nivel", m.IdNivel);
            cmd.Parameters.AddWithValue("@instructor", m.IdInstructor);
            cmd.Parameters.AddWithValue("@fecha", m.FechaMatricula);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Verifica si ya existe una matrícula activa para el alumno en el mismo nivel
    // Evita registros duplicados — un alumno no puede estar dos veces en el mismo nivel
    public bool ExisteMatriculaDuplicada(int idAlumno, int idNivel)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT COUNT(*) FROM MATRICULAS 
              WHERE IDALUMNO = @alumno AND IDNIVEL = @nivel", con))
        {
            cmd.Parameters.AddWithValue("@alumno", idAlumno);
            cmd.Parameters.AddWithValue("@nivel", idNivel);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Obtiene todas las matrículas con información de alumno, nivel e instructor
    // Usa INNER JOIN para traer los nombres relacionados en una sola consulta
    public List<_Matricula> ObtenerTodos()
    {
        var lista = new List<_Matricula>();
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT M.IDMATRICULA, M.IDALUMNO, M.IDNIVEL, M.IDINSTRUCTOR, M.FECHAMATRICULA,
                     A.Nombre + ' ' + A.APELLIDO AS NombreAlumno,
                     N.NOMBRENIVEL,
                     I.NOMBRE + ' ' + I.APELLIDO AS NombreInstructor
              FROM MATRICULAS M
              INNER JOIN ALUMNOS A ON M.IDALUMNO = A.IDALUMNO
              INNER JOIN NIVELES N ON M.IDNIVEL = N.IDNIVEL
              INNER JOIN INSTRUCTORES I ON M.IDINSTRUCTOR = I.IDINSTRUCTOR", con))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                _Matricula m = new _Matricula();
                m.IdMatricula = reader.GetInt32(0);
                m.IdAlumno = reader.GetInt32(1);
                m.IdNivel = reader.GetInt32(2);
                m.IdInstructor = reader.GetInt32(3);
                m.FechaMatricula = reader.GetDateTime(4);
                m.NombreAlumno = reader.GetString(5);
                m.NombreNivel = reader.GetString(6);
                m.NombreInstructor = reader.GetString(7);
                lista.Add(m);
            }
        }
        return lista;
    }

    // TODO: Elimina una matrícula por su ID
    // Solo se puede eliminar si no tiene pagos asociados (verificar antes de llamar)
    public bool Eliminar(int idMatricula)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "DELETE FROM MATRICULAS WHERE IDMATRICULA=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idMatricula);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Verifica si una matrícula tiene pagos registrados
    // Se usa antes de eliminar para respetar la integridad referencial
    public bool TienePagos(int idMatricula)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "SELECT COUNT(*) FROM PAGOS WHERE IDMATRICULA=@id", con))
        {
            cmd.Parameters.AddWithValue("@id", idMatricula);
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Obtiene el nivel actual del alumno (el más reciente por ID de matrícula)
    // Se usa en frmAlumnos para mostrar el nivel actual al seleccionar un alumno
    public string ObtenerNivelAlumno(int idAlumno)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT TOP 1 N.NOMBRENIVEL
              FROM MATRICULAS M
              INNER JOIN NIVELES N ON M.IDNIVEL = N.IDNIVEL
              WHERE M.IDALUMNO = @id
              ORDER BY M.IDMATRICULA DESC", con))
        {
            cmd.Parameters.AddWithValue("@id", idAlumno);
            var resultado = cmd.ExecuteScalar();
            if (resultado != null)
                return resultado.ToString();
            else
                return "Sin nivel asignado";
        }
    }

    // TODO: Actualiza el nivel de una matrícula existente al promover un alumno
    // Se llama desde frmAlumnos cuando el director presiona el botón Promover
    public bool ActualizarNivel(int idMatricula, int idNivelNuevo)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            "UPDATE MATRICULAS SET IDNIVEL=@nivel WHERE IDMATRICULA=@id", con))
        {
            cmd.Parameters.AddWithValue("@nivel", idNivelNuevo);
            cmd.Parameters.AddWithValue("@id", idMatricula);
            int filas = cmd.ExecuteNonQuery();
            if (filas > 0)
                return true;
            else
                return false;
        }
    }

    // TODO: Obtiene la matrícula activa de un alumno específico
    // Se usa para verificar si el alumno ya tiene una matrícula antes de crear una nueva
    public _Matricula ObtenerMatriculaActiva(int idAlumno)
    {
        using (var con = Conexion.ObtenerConexion())
        using (var cmd = new SqlCommand(
            @"SELECT TOP 1 IDMATRICULA, IDALUMNO, IDNIVEL, IDINSTRUCTOR, FECHAMATRICULA
              FROM MATRICULAS
              WHERE IDALUMNO = @id
              ORDER BY IDMATRICULA DESC", con))
        {
            cmd.Parameters.AddWithValue("@id", idAlumno);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    _Matricula m = new _Matricula();
                    m.IdMatricula = reader.GetInt32(0);
                    m.IdAlumno = reader.GetInt32(1);
                    m.IdNivel = reader.GetInt32(2);
                    m.IdInstructor = reader.GetInt32(3);
                    m.FechaMatricula = reader.GetDateTime(4);
                    return m;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}