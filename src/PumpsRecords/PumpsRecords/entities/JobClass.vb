Namespace entities
    Public Class JobClass

        Private _IDClass As Integer = -1
        Private _ClassName As String
        Private _active As Boolean = True

        Public Property Active() As Boolean
            Get
                Return _active
            End Get
            Set(ByVal value As Boolean)
                _active = value
            End Set
        End Property

        Public Property ClassName() As String
            Get
                Return _ClassName
            End Get
            Set(ByVal value As String)
                _ClassName = value
            End Set
        End Property

        Public Property IDClass() As Integer
            Get
                Return _IDClass
            End Get
            Set(ByVal value As Integer)
                _IDClass = value
            End Set
        End Property

    End Class
End Namespace
