using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Diagnostics;


namespace MyConexion
{
    public class ConexionSQL
    {
        //objetos necesarios para la conexion y el insert
        public SqlConnection conexionSQL;
        public SqlCommand comandosSQL;
        public SqlDataAdapter adapter;
        public Exception exception;
        public SqlDataReader leerbd;
        public Object existe;
        public DataSet datos;
        string ruta = "C:\\Program Files\\Microsoft SQL Server\\MSSQL16.SQLEXPRESS";
        string servidor = "";
        public ConexionSQL(string db, string servidor = null)
        {

            if (servidor == null)
            {
                Console.WriteLine("SERVIDOR =NULL");
                //servidor = "local";
                servidor = getMachineName();
                if (Directory.Exists(this.ruta))
                {
                    servidor += "\\SQLEXPRESS";
                    //servidor = this.getMachineName() + "\\SQLEXPRESS";
                }
                else
                {
                    servidor = "(local)";
                }
            }
            /*if (servidor == "(local)")
            {
                servidor += "\\SQLEXPRESS";
            }*/
            this.servidor = servidor;
            Debug.WriteLine(servidor);
            this.conectar(db, servidor);
        }

        public bool Open()
        {
            try
            {
                this.conexionSQL.Open();
                Console.WriteLine("Conexion Abierta");
                return true;
            } catch (Exception ex) {
                exception = ex;
                Console.WriteLine("ERROR AL ABRIR");
                return false;
            }
        }
        public bool Close()
        {
            try
            {
                this.conexionSQL.Close();
                Console.WriteLine("Conexion Cerrada");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void AlterRow(string table, int rowNumber, string[] campos, string[] nuevosCampos)
        {
            int index = 0;
            foreach(string campo in campos)
            {
                string nuevoCampo = campo.Replace("'", "");
                if (nuevoCampo.Length!=campo.Length)
                {
                    //es string lo que acabamos de recibir
                    datos.Tables[table].Rows[rowNumber][campo] = nuevosCampos[index];
                }
                else if(nuevoCampo.Split(".").Length==2)
                {
                    //recibimos un decimal
                    datos.Tables[table].Rows[rowNumber][campo] = double.Parse(nuevosCampos[index]);
                }
                else
                {
                    //recibimos un entero
                    datos.Tables[table].Rows[rowNumber][campo] = Convert.ToInt32(nuevosCampos[index]);
                }
                index++;
            }
        }
        public void actualizarCambios(string tabla)
        {
            adapter.Update(datos, tabla);
        }
        public bool ejecutarQuery(string strSQL, int modo = 1, string tabla=null)
        {
            //modos:
            //    1----insert
            //    2----select
            //    3----select con ExecuteScalar()
            //    4----select con SqlDataAdapter y DataSet
            Console.WriteLine(strSQL);
            try
            {
                this.comandosSQL = new SqlCommand(strSQL, this.conexionSQL);

                if (modo == 2)
                {
                    this.leerbd = comandosSQL.ExecuteReader();
                }
                else if (modo == 3)
                {
                    this.existe = new Object();
                    this.existe = this.comandosSQL.ExecuteScalar();
                }
                else if (modo == 4)
                {
                    this.datos = new DataSet();
                    this.adapter = new SqlDataAdapter(this.comandosSQL);
                    this.adapter.Fill(this.datos, tabla);
                }
                else
                {
                    this.comandosSQL.ExecuteNonQuery();
                }

                Console.WriteLine("CONSULTA EXITOSA");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("CONSULTA FALLIDA" + ex.Message);
                
                this.exception = ex;

                return false;
            }
        }

        public void conectar(string db, string servidor)
        {
            String strConexion;
            strConexion =
                $"Data Source={servidor};" + // servidor
                $"Initial Catalog={db};" + // base de datos

                ////Campos extras en caso de necesitar:
                //$"User:{usuario}" +
                //$"Pasword={psw}"

                "Integrated Security=True;";// configuracion de seguridad

            Console.WriteLine(strConexion);
            this.conexionSQL = new SqlConnection(strConexion);
        }
        public string getMachineName()
        {

            // Obtiene el nombre del equipo
            string nombreMaquina = Environment.MachineName;

            // Muestra el nombre en la consola
            //"El nombre de la máquina es: " +
            return (nombreMaquina);

        }
        public string makeAlertText(string messaje)
        {
            return $"<script>alert(`{messaje}`)</script>";
        }

        public SqlDataReader getReader()
        {
            return leerbd;
        }
        
    }
}