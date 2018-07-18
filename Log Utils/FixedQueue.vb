Public Class FixedQueue(Of T)
   Inherits Queue(Of T)
   Private QueueSize As Integer

   Public Sub New(ByVal capacity As Integer)
      MyBase.New(capacity)
      QueueSize = capacity
   End Sub

   Public Shadows Sub Enqueue(ByVal val As T)
      If Me.Count >= QueueSize Then
         Me.Dequeue()
      End If
      MyBase.Enqueue(val)
   End Sub
End Class
