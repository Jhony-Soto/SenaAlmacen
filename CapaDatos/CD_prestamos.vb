Imports System.Data.SqlClient
Imports System.Data.SqlClient.SqlTransaction
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

    Public Function prestamoXcliente(ByVal idCliente As Integer) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_prestamosXcliente_eliminar_Datos", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@variable", SqlDbType.VarChar).Value = idCliente
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

    Public Sub deletePrestamosXcliente(ByVal idPrestamo As Integer)

        conexion = objConexion.Conectar
        conexion.Open()

        'Inicia el transaccion sql
        Dim transactionSQL = conexion.BeginTransaction
        Try
            'procediemiento N°1
            Dim comandos = New SqlCommand("sp_tbl_prestamo_elemento_delete_datos", conexion, transactionSQL)
            comandos.CommandType = CommandType.StoredProcedure

            With comandos.Parameters
                .Add("@variable", SqlDbType.VarChar).Value = idPrestamo
            End With

            comandos.ExecuteNonQuery()

            'procediemiento N°2
            comandos = New SqlCommand("sp_tbl_prestamos_delete", conexion, transactionSQL)
            comandos.CommandType = CommandType.StoredProcedure

            With comandos.Parameters
                .Add("@variable", SqlDbType.VarChar).Value = idPrestamo
            End With

            comandos.ExecuteNonQuery()

            transactionSQL.Commit()

            conexion.Close()
        Catch ex As SqlException
            transactionSQL.Rollback()
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Sub

End Class
