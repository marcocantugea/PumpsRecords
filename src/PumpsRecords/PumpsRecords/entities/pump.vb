
Namespace entities


    Public Class pump

        Private _IDPump As Integer = -1
        Private _Description As String
        'Private _Active As Integer = -1
        Private _Status As String


        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        'Public Property Active() As Integer
        '    Get
        '        Return _Active
        '    End Get
        '    Set(ByVal value As Integer)
        '        _Active = value
        '    End Set
        'End Property

        Public Property Description() As String
            Get
                Return _Description
            End Get
            Set(ByVal value As String)
                _Description = value
            End Set
        End Property

        Public Property IDPump() As Integer
            Get
                Return _IDPump
            End Get
            Set(ByVal value As Integer)
                _IDPump = value
            End Set
        End Property


    End Class
End Namespace