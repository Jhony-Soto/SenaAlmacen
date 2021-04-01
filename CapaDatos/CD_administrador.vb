Imports System.Data.SqlClient

Public Class CD_administrador

    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    'Retorna todos los administradores
    Public Function getAll() As DataSet

        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_administradores_select", conexion)
            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "admin")
            Return dataset
        Catch ex As Exception
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try


    End Function


    'Insertar un nuevo administrador
    Public Function saveAdministrador(ByVal datos As Array) As Integer

        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_admin_insert", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@documento", SqlDbType.VarChar).Value = datos(0)
                .Add("@nombres", SqlDbType.VarChar).Value = datos(1)
                .Add("@apellidos", SqlDbType.VarChar).Value = datos(2)
                .Add("@email", SqlDbType.VarChar).Value = datos(3)
                .Add("@telefonomovil", SqlDbType.VarChar).Value = datos(4)
                .Add("@contrasenia", SqlDbType.VarChar).Value = datos(5)
                .Add("@fecha_expedicion", SqlDbType.Date).Value = datos(6)
                .Add("@foto", SqlDbType.VarChar).Value = datos(7)
                .Add("@tipo_documento", SqlDbType.Int).Value = datos(8)
                .Add("@estado", SqlDbType.VarChar).Value = datos(9)
                .Add("@tipo_admin", SqlDbType.VarChar).Value = datos(10)
            End With
            conexion.Open()
            dataAdapter.SelectCommand.ExecuteNonQuery()
            conexion.Close()

            Return 1
        Catch ex As SqlException
            Return ex.Number
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

    'Cambia el estado de un administrador de 0 a 1
    Public Function inactivos_activar_cliente(ByVal dato As Integer)
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_administradores_activar", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@documento", SqlDbType.VarChar).Value = dato
            End With
            conexion.Open()
            dataAdapter.SelectCommand.ExecuteNonQuery()
            conexion.Close()

            Return 1
        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try
    End Function

    'recuperar contraseña de administrador
    Public Function recuperarContraseña(ByVal datos As Array) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_admin_contrasenia", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@documento", SqlDbType.VarChar).Value = datos(0)
                .Add("@fecha", SqlDbType.Date).Value = datos(1)
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

    Public Function ValidarLogin(ByVal datos As Array) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_usuario_selectPK", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@documento", SqlDbType.VarChar).Value = datos(0)
                .Add("@contrasenia", SqlDbType.VarChar).Value = datos(1)
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
