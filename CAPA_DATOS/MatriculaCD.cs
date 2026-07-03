using Microsoft.Data.SqlClient;

namespace CAPA_DATOS;

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

    // Campos extra para mostrar en la grilla
    public string NombreAlumno { get; set; } = string.Empty;
    public string NombreNivel { get; set; } = string.Empty;
    public string NombreInstructor { get; set; } = string.Empty;

    public _Matricula()
    {
        this.fechaMatricula = DateTime.Today;
    }

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

public class MatriculaCD
{
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
}