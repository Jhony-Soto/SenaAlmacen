Public Class CE_administrador
    Private _documento As Integer
    Private _nombres As String
    Private _apellidos As String
    Private _email As String
    Private _telefono As String
    Private _contrasenia As String
    Private _fecha_expedicion As Date
    Private _foto As String
    Private _tipo_documento As Integer
    Private _estado As String
    Private _tipo_admin As String

    'metodos de encapsulamiento 
    'DOCUMENTO
    Public Property documento As Integer
        Get
            Return _documento
        End Get
        Set(value As Integer)
            _documento = value
        End Set
    End Property

    'NOMBRES
    Public Property nombres As String
        Get
            Return _nombres
        End Get
        Set(value As String)
            _nombres = value
        End Set
    End Property

    'APELLIDOS
    Public Property apellidos As String
        Get
            Return _apellidos
        End Get
        Set(value As String)
            _apellidos = value
        End Set
    End Property

    '_email
    Public Property email As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
        End Set
    End Property

    '_telefono
    Public Property telefono As String
        Get
            Return _telefono
        End Get
        Set(value As String)
            _telefono = value
        End Set
    End Property

    '_contrasenia
    Public Property contrasenia As String
        Get
            Return _contrasenia
        End Get
        Set(value As String)
            _contrasenia = value
        End Set
    End Property


    '_fecha_expedicion
    Public Property fecha_expedicion As Date
        Get
            Return _fecha_expedicion
        End Get
        Set(value As Date)
            _fecha_expedicion = value
        End Set
    End Property


    '_foto
    Public Property foto As String
        Get
            Return _foto
        End Get
        Set(value As String)
            _foto = value
        End Set
    End Property


    '_tipo_documento
    Public Property tipo_documento As Integer
        Get
            Return _tipo_documento
        End Get
        Set(value As Integer)
            _tipo_documento = value
        End Set
    End Property

    '_estado
    Public Property estado As String
        Get
            Return _estado
        End Get
        Set(value As String)
            _estado = value
        End Set
    End Property


    '_tipo_admin
    Public Property tipo_admin As String
        Get
            Return _tipo_admin
        End Get
        Set(value As String)
            _tipo_admin = value
        End Set
    End Property

End Class
