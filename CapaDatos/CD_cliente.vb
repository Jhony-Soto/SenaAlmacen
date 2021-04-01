Imports System.Data.SqlClient
Public Class CD_cliente
    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    Public Function getClientesXfecha(ByVal fecha As Date) As DataSet
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("ps_tbl_clientes_eliminar_datos", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@variable", SqlDbType.VarChar).Value = fecha
            End With

            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "datos")

            Return dataset
        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try
    End Function
End Class
