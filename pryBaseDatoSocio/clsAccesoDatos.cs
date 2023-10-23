using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace pryBaseDatoSocio
{
    internal class clsAccesoDatos
    {
        OleDbConnection conexionBD;
        OleDbCommand comandoBD; //Sirve para datos, editar y borrar 
        OleDbDataReader lectorBD;

        string cadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
            "Data Source = ..\\..\\Resources\\EL_CLUB.accdb";

        public string estadoConexion="";
        public string datosTabla;

        public void ConectarBD()
        {
            try
            {
                conexionBD = new OleDbConnection();
                conexionBD.ConnectionString = cadenaConexion;
                conexionBD.Open();
                estadoConexion = "Conectado";

            }
            catch (Exception EX)
            {
                estadoConexion = "Error:" + EX.Message;
            }
        }

        public void TraerDatos()
        {
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBD = comandoBD.ExecuteReader();

            if (lectorBD.HasRows)
            {
                while (lectorBD.Read())
                {
                    datosTabla += "-" + lectorBD[0]; //El 0 muestra la primer columna(Los ID)
                }
            }
        }


        public void TraerDatos(DataGridView grilla)
        {
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBD = comandoBD.ExecuteReader();

            grilla.Columns.Add("Nombre", "Nombre");
            grilla.Columns.Add("Apellido", "Apellido");
            grilla.Columns.Add("Edad", "Edad");

            if (lectorBD.HasRows)
            {
                while (lectorBD.Read())
                {
                    datosTabla += "-" + lectorBD[0]; //El 0 muestra la primer columna(Los ID)
                    grilla.Rows.Add(lectorBD[1], lectorBD[2], lectorBD[3]);
                }
            }
        }

        public void BuscarPorCodigo(int codigoSocio)
        {
            comandoBD = new OleDbCommand();

            comandoBD.Connection = conexionBD;
            comandoBD.CommandType = System.Data.CommandType.TableDirect;
            comandoBD.CommandText = "SOCIOS";

            lectorBD = comandoBD.ExecuteReader();

            bool seEncuentra = false;

            if (lectorBD.HasRows)
            {
                while (lectorBD.Read())
                {
                    if (int.Parse(lectorBD[0].ToString()) == codigoSocio)
                    {
                        MessageBox.Show("Cliente existe", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        seEncuentra = true;
                        break;
                    }
                }

                if (seEncuentra == false)
                {
                    MessageBox.Show("Cliente no existe", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
