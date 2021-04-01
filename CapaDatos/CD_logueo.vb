Imports System.Data.SqlClient
Public Class CD_logueo
    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    'Guarda el resgistro del usuario que inicio session
    Public Sub inserLogueo(ByVal datos As Array)
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_logeo_insert", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@administrador", SqlDbType.VarChar).Value = datos(0)
                .Add("@hora_inicio", SqlDbType.DateTime).Value = datos(1)
            End With

            conexion.Open()
            dataAdapter.SelectCommand.ExecuteNonQuery()
            conexion.Close()

        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try
    End Sub


    'Ultimo registro de la tabla logueo
    Public Function idUltimoRegistro() As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_logeo_order_by", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

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

    'Se actualiza la hora se cierrre de la session
    Public Sub dateFinishSession(ByVal id As Integer)
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_logeo_update", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@codigo", SqlDbType.Int).Value = id
                .Add("@hora_final", SqlDbType.DateTime).Value = Date.Now
            End With

            conexion.Open()
            dataAdapter.SelectCommand.ExecuteNonQuery()
            conexion.Close()

        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try
    End Sub

End Class
