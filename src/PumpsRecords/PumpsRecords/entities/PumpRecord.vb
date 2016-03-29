
Namespace entities
    Public Class PumpRecord

        Private _IDRecord As Integer = -1
        Private _DateCaptured As Date
        Private _DatePumpRecord As Date
        Private _RecordCreatedBy As String
        Private _RecordModifyBy As String
        Private _IDPump As Integer
        Private _PumpDesc As String
        Private _DischargePressure As Double
        Private _Amps As Double
        Private _Comment As String
        Private _Flag As Boolean = False
        Private _Active As Boolean = True

        Public Property Active() As Boolean
            Get
                Return _Active
            End Get
            Set(ByVal value As Boolean)
                _Active = value
            End Set
        End Property

        Public Property Flag() As Boolean
            Get
                Return _Flag
            End Get
            Set(ByVal value As Boolean)
                _Flag = value
            End Set
        End Property

        Public Property Comment() As String
            Get
                Return _Comment
            End Get
            Set(ByVal value As String)
                _Comment = value
            End Set
        End Property


        Public Property Amps() As Double
            Get
                Return _Amps
            End Get
            Set(ByVal value As Double)
                _Amps = value

            End Set
        End Property

        Public Property DischargePressure() As Double
            Get
                Return _DischargePressure
            End Get
            Set(ByVal value As Double)
                _DischargePressure = value
            End Set
        End Property

        Public Property PumpDesc() As String
            Get
                Return _PumpDesc
            End Get
            Set(ByVal value As String)
                _PumpDesc = value
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

        Public Property RecordModifyBy() As String
            Get
                Return _RecordModifyBy
            End Get
            Set(ByVal value As String)
                _RecordModifyBy = value
            End Set
        End Property


        Public Property RecordCreatedBy() As String
            Get
                Return _RecordCreatedBy
            End Get
            Set(ByVal value As String)
                _RecordCreatedBy = value
            End Set
        End Property

        Public Property DatePumpRecord() As Date
            Get
                Return _DatePumpRecord
            End Get
            Set(ByVal value As Date)
                _DatePumpRecord = value
            End Set
        End Property

        Public Property DateCaptured() As Date
            Get
                Return _DateCaptured
            End Get
            Set(ByVal value As Date)
                _DateCaptured = value
            End Set
        End Property

        Public Property IDRecord() As Integer
            Get
                Return _IDRecord
            End Get
            Set(ByVal value As Integer)
                _IDRecord = value
            End Set
        End Property


    End Class
End Namespace
