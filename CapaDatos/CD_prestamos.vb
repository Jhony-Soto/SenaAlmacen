Imports System.Data.SqlClient
Public Class CD_prestamos
    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    Public Function prestamosActivos(ByVal fecha As Date) As DataSet
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_prestamo_PrestamosActivos", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@fechahoy", SqlDbType.Date).Value = fecha
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
