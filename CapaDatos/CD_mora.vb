Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlTransaction
Public Class CD_mora
    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    'Obtiene los codigos de todas las moras de un cliente
    Public Function getIdsMorasXcliente(ByVal idCliente As Integer) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_mora_Consultar", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@codcliente", SqlDbType.VarChar).Value = idCliente
            End With

            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "datos")

            Return dataset.Tables(0)
        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try
    End Function

End Class
