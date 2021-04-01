Imports System.Data.SqlClient
Public Class CD_configuracion

    Dim objConexion As New BDConexion 'Clase de la conexion
    Dim dataAdapter As SqlDataAdapter
    Dim conexion As SqlConnection 'Atributo con la conexion.

    Function getAll() As DataSet
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tblConfiguraicones_Select", conexion)
            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "data")
            Return dataset
        Catch ex As Exception
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

    Function centroActual() As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_centroActual_select", conexion)
            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "data")
            Return dataset.Tables(0)
        Catch ex As Exception
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

    Function cargar_centro(ByVal id As Integer) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_centro_selectFK", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@codciudad", SqlDbType.VarChar).Value = id
            End With

            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "data")
            Return dataset.Tables(0)

        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

    Function cargar_ciudad(ByVal id As Integer) As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_ciudad_select", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@coddepartamento", SqlDbType.VarChar).Value = id
            End With

            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "data")
            Return dataset.Tables(0)

        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

    Function cargar_departamentos() As DataTable
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_departamento_selct", conexion)

            Dim dataset As New DataSet
            dataAdapter.Fill(dataset, "data")
            Return dataset.Tables(0)

        Catch ex As SqlException
            Throw ex
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try

    End Function

    Function saveCentroActual(ByVal datos As Array) As Integer
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_centroActual_insert", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@codigo", SqlDbType.VarChar).Value = datos(0)
                .Add("@nombre", SqlDbType.VarChar).Value = datos(1)
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

    Function saveRutasConfig(ByVal datos As Array) As Integer
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_configuracion_insert", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@permisos", SqlDbType.VarChar).Value = datos(0)
                .Add("@elementos", SqlDbType.VarChar).Value = datos(1)
                .Add("@clientes", SqlDbType.VarChar).Value = datos(2)
                .Add("@administradores", SqlDbType.VarChar).Value = datos(3)
                .Add("@pdf", SqlDbType.VarChar).Value = datos(4)
                .Add("@ruta_backup", SqlDbType.VarChar).Value = datos(5)
                .Add("@salidas", SqlDbType.VarChar).Value = datos(6)
                .Add("@seguros", SqlDbType.VarChar).Value = datos(7)
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

    Public Sub updateStateBackup(ByVal datos As Array)
        Try
            conexion = objConexion.Conectar
            dataAdapter = New SqlDataAdapter("sp_tbl_configuiracion_backup_sistema", conexion)
            dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            With dataAdapter.SelectCommand.Parameters
                .Add("@fecha_copia", SqlDbType.Date).Value = datos(0)
                .Add("@estado_copia", SqlDbType.VarChar).Value = datos(1)
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

    Public Function generateBackup(ByVal procedure As String) As Integer
        Try
            conexion = objConexion.Conectar
            Dim comand = New SqlCommand(procedure, conexion) 'Strin con el que se genera la copia de seguridad
            conexion.Open()
            comand.ExecuteNonQuery()
            conexion.Close()

            Return 1
        Catch ex As SqlException
            Return ex.Number
        Finally
            conexion.Dispose()
            dataAdapter.Dispose()
        End Try
    End Function

End Class
