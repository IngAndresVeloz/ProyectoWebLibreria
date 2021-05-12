using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Design;
using DA;


namespace Logic
{
    public class LibroLO
    {
        public SqlConnection conexion;
        public SqlTransaction transaccion;
        public string error;

        public LibroLO()
        {
            this.conexion = Conexion.getConexion();//Obtiene la conexion a la base de datos
        }
        /************Agregar Libro*****************/
        public bool agregarVideo(LibroDis libro, string Identificador)
        {
            //primero se consulta si esta el libro en la base de datos

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandText = "select *from InfoLibros";
            SqlDataReader registro = comando.ExecuteReader();// Lee el dato consultado en registro
            LibroDis final = new LibroDis(); //variable vacia para guardar la informacion del libro que tenga a coicidencia
            final.Titulo = "";
            final.Anio = 0;
            final.Autor = "";
            final.Estado = "";
            while (registro.Read()) //se analiza la base de datos para encontrar la corresponidente igualdad
            {
                LibroDis Libro = new LibroDis();
                Libro.Titulo = registro.GetString(0);
                Libro.Anio = registro.GetInt32(1);
                Libro.Autor = registro.GetString(2);
                if (Libro.Estado == "LIBRE") //si el libro esta libre se pone en la lista sin esta rentado no se guarda
                {
                    Libro.Estado = registro.GetString(6);
                }

                if (Identificador == Libro.Titulo)
                {
                    final.Titulo = Libro.Titulo;
                }
            }
            if (final.Titulo == Identificador)
            {
                registro.Close();
                return false;
            }
            else
            {
                //Acceso a base de datos
                registro.Close(); //primero se cierra el registro 
                bool agregar = false;
                comando.Connection = conexion; //comando de conexion
                comando.CommandText = "insert into dblibrary values(@titulo, @anio, @autor, @estado)";

                /*************Preparando para inserta los datos del Libro en la tabla de la base de datos*************/

                comando.Parameters.AddWithValue("@titulo", libro.Titulo);
                comando.Parameters.AddWithValue("@anio", libro.Anio);
                comando.Parameters.AddWithValue("@autor", libro.Autor);
                comando.Parameters.AddWithValue("@estado", libro.Estado);
                try
                {
                    comando.ExecuteNonQuery(); //Realizar la operacion de insertar en la base de datos
                    agregar = true;
                }
                catch (SqlException ex)
                {
                    this.error = ex.Message;
                }

                return agregar; //Se inserto correctamente
            }


        }
        /*******************Consultar Libro********************/
        public LibroDis ConsultarLibro(string Identificador)
        {

            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandText = "select *from InfoLibros";
            SqlDataReader registro = comando.ExecuteReader();// Lee el dato consultado en registro
            LibroDis final = new LibroDis(); //variable vacia para guardar la informacion del libro que tenga a coicidencia
            final.Titulo = "";
            final.Anio = 0;
            final.Autor = "";
            final.Estado = "";
            while (registro.Read()) //se analiza la base de datos para encontrar la corresponidente igualdad
            {
                LibroDis libro = new LibroDis();
                libro.Titulo = registro.GetString(0);
                libro.Anio = registro.GetInt32(1);
                libro.Autor = registro.GetString(2);
                //si es una serie se ponen los datos de capitulos y temporadas, si es pelicula no se ponen
                if (libro.Estado == "LIBRE")
                {
                    libro.Estado = registro.GetString(3);
                }
                //de acuerdo al identificador, se guarda el Libro consultado en la variable final
                if (Identificador == libro.Titulo)
                {
                    final.Titulo = libro.Titulo;
                    final.Anio = libro.Anio;
                    final.Autor = libro.Autor;
                    if (libro.Estado == "LIBRE")
                    {
                        final.Estado = libro.Estado;
                    }
                }
            }
            if (final.Titulo == Identificador)
            {
                registro.Close();
                return final;
            }
            else
            {
                registro.Close();
                return null;
            }



        }
        /***************se visualiza los datos de todos los Libros***********/
        public List<LibroDis> listarLibro()
        {

            List<LibroDis> listaVideos = new List<LibroDis>(); //se crea lista para guardar
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandText = "select *from InfoLibros";
            SqlDataReader registro = comando.ExecuteReader(); //Leer Datos de la base de Datos
            while (registro.Read())
            {
                LibroDis libro = new LibroDis();
                libro.Titulo = registro.GetString(0);
                libro.Anio = registro.GetInt32(1);
                libro.Autor = registro.GetString(2);
                //si es una serie se ponen los datos de capitulos y temporadas, si es pelicula no se ponen
                if (libro.Estado == "LIBRE")
                {
                    libro.Estado = registro.GetString(3);
                }

                listaVideos.Add(libro); // se agrega a la lista cada persona que encuentra
            }
            registro.Close();
            return listaVideos;

        }
    }
}
