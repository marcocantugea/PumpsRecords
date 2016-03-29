
Namespace entities
Public Class JobClassCollection

        Implements IEnumerable, IEnumerator, ICollection

        Private position As Integer = -1
        Private _Items As New List(Of JobClass)
        'Private _ADODDR As New data.ADO.ADOPumpsRecords

        Public ReadOnly Property Items() As List(Of JobClass)
            Get
                Return _Items
            End Get
        End Property


        Public Sub Add(ByVal item As JobClass)
            Add(item, False)
        End Sub

        Public Sub Add(ByVal item As JobClass, ByVal AddToTable As Boolean)
            If Not IsNothing(item) Then
                If AddToTable Then
                    '_ADODDR.InsertPump(item)
                End If
                _Items.Add(item)
            End If
        End Sub
        Public Sub Remove(ByVal item As JobClass)
            Remove(item, False)
        End Sub

        Public Sub Remove(ByVal item As JobClass, ByVal applyondatabase As Boolean)
            If Not IsNothing(item) Then
                If item.IDClass > 0 Then
                    If applyondatabase Then
                        '_ADODDR.DeletePump(item)
                    End If
                    For Each i As JobClass In _Items
                        If i.IDClass = item.IDClass Then
                            _Items.Remove(i)
                        End If
                    Next
                End If
            End If

        End Sub

        Public Sub CopyTo(ByVal array As System.Array, ByVal index As Integer) Implements System.Collections.ICollection.CopyTo

        End Sub

        Public ReadOnly Property Count() As Integer Implements System.Collections.ICollection.Count
            Get
                Return _Items.Count
            End Get
        End Property

        Public ReadOnly Property IsSynchronized() As Boolean Implements System.Collections.ICollection.IsSynchronized
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property SyncRoot() As Object Implements System.Collections.ICollection.SyncRoot
            Get
                Return Me
            End Get
        End Property

        Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return CType(Me, IEnumerator)
        End Function

        Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
            Get
                Return _Items.GetEnumerator.Current
            End Get
        End Property

        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            position += 1
            Return (position <= _Items.Count)
        End Function

        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            position = -1
        End Sub
End Class
End Namespace
