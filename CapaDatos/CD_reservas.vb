Imports System.Data.SqlClient
Public Class CD_reservas
    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    Public Function reservasActivas(ByVal fecha As Date) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_reserva_select_fecha_prestamo", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@fecha_actual", SqlDbType.Date).Value = fecha
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
