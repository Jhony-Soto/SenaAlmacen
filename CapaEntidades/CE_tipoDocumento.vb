Public Class CE_tipoDocumento
    Private _codigo As Integer
    Private _nombre As String
    Private _estado As String


    'metodos de encapsulamiento 
    'DOCUMENTO
    Public Property codigo As Integer
        Get
            Return _codigo
        End Get
        Set(value As Integer)
            _codigo = value
        End Set
    End Property

    'NOMBRES
    Public Property nombre As String
        Get
            Return _nombre
        End Get
        Set(value As String)
            _nombre = value
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

End Class
