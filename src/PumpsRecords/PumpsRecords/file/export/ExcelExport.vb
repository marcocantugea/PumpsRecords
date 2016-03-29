Imports System
Imports System.Globalization
Imports System.Reflection ' For Missing.Value and BindingFlags
Imports System.Runtime.InteropServices ' For COMException
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.Office.Interop
Imports System.Threading
Namespace file.export
    Public Class ExcelExport

        Dim xlApp As Application
        Dim xlWorkBook As Workbook
        Dim xlSheet As Worksheet
        Dim Document As String
        Dim oldCI As CultureInfo

        Public Sub New(ByVal Document As String)
            Me.Document = Document
        End Sub
        Public Sub New()

        End Sub
        Public Sub OpenDocument()
            Try
                'Try
                xlApp = New Application
                ''xlApp.Visible = True
                xlApp.Visible = True
                oldCI = Thread.CurrentThread.CurrentCulture
                Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
                Dim misValue As Object = System.Reflection.Missing.Value

                xlWorkBook = xlApp.Workbooks.Add(misValue)
                Dim xlworksheet As Microsoft.Office.Interop.Excel.Worksheet

                xlSheet = xlApp.Sheets("sheet1")
                xlSheet.Activate()


            Catch ex As Exception
                Throw New Exception("Error on OpenDocument() Msg:" & ex.Message.ToString & " Document:" & Document)
            End Try
        End Sub

        Public Sub ExportData(ByVal l_list As List(Of PumpsRecords.entities.PumpRecord))
            OpenDocument()
            ' set header of the table
            xlSheet.Cells(1, 1).value = "Pump"
            xlSheet.Cells(1, 2).value = "Date"
            xlSheet.Cells(1, 3).value = "Discharge Pump"
            xlSheet.Cells(1, 4).value = "Amps"
            xlSheet.Cells(1, 5).value = "Comments"



            'export the data displayed
            ' start on the row 2
            Dim x As Integer = 2
            For Each item As PumpsRecords.entities.PumpRecord In l_list
                xlSheet.Cells(x, 1).value = item.PumpDesc
                xlSheet.Cells(x, 2).value = item.DatePumpRecord
                xlSheet.Cells(x, 3).value = item.DischargePressure
                xlSheet.Cells(x, 4).value = item.Amps
                xlSheet.Cells(x, 5).value = item.Comment
                x = x + 1

            Next

            xlSheet.Rows.Item(1).EntireColumn.AutoFit()
            xlSheet.Rows.Item(2).EntireColumn.AutoFit()
            ' create chart discharge pressure
            Dim chartpage As Microsoft.Office.Interop.Excel.Chart
            Dim xlCharts As Microsoft.Office.Interop.Excel.ChartObjects
            Dim myChart As Microsoft.Office.Interop.Excel.ChartObject
            Dim ChartRange As Microsoft.Office.Interop.Excel.Range

            xlCharts = xlSheet.ChartObjects
            myChart = xlCharts.Add(x * 15, x * 20, 300, 250)
            chartpage = myChart.Chart
            ChartRange = xlSheet.Range("C2", "C" & x - 1)

            chartpage.SetSourceData(Source:=ChartRange)
            chartpage.ChartType = XlChartType.xlLine

            Dim axis As Excel.Axis = CType(chartpage.Axes(Excel.XlAxisType.xlValue, XlAxisGroup.xlPrimary), Excel.Axis)
            axis.HasTitle = True
            axis.AxisTitle.Text = "Presure bars"

            chartpage.SeriesCollection(1).name = "Discharge Pump"
            chartpage.SeriesCollection(1).xValues = ("=Sheet1!$B$2:$B$" & x - 1)

            ' create chart amps
            Dim chartpage2 As Microsoft.Office.Interop.Excel.Chart
            Dim xlCharts2 As Microsoft.Office.Interop.Excel.ChartObjects
            Dim myChart2 As Microsoft.Office.Interop.Excel.ChartObject
            Dim ChartRange2 As Microsoft.Office.Interop.Excel.Range

            xlCharts2 = xlSheet.ChartObjects
            Dim chart_y_pos As Integer = x * 20 + 300
            Dim chart_x_pos As Integer = x * 15
            myChart2 = xlCharts2.Add(chart_x_pos, chart_y_pos, 300, 250)
            chartpage2 = myChart2.Chart
            ChartRange2 = xlSheet.Range("D2", "D" & x - 1)

            chartpage2.SetSourceData(Source:=ChartRange2)
            chartpage2.ChartType = XlChartType.xlLine

            Dim axis2 As Excel.Axis = CType(chartpage2.Axes(Excel.XlAxisType.xlValue, XlAxisGroup.xlPrimary), Excel.Axis)
            axis2.HasTitle = True
            axis2.AxisTitle.Text = "amp"

            chartpage2.SeriesCollection(1).name = "AMPS"
            chartpage2.SeriesCollection(1).xValues = ("=Sheet1!$B$2:$B$" & x - 1)




        End Sub


        Public Sub CloseDocument()
            xlWorkBook.Close()
            xlApp.Quit()
            Thread.CurrentThread.CurrentCulture = oldCI

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlSheet)
        End Sub

        Private Sub releaseObject(ByVal obj As Object)
            Try
                Marshal.ReleaseComObject(obj)
                obj = Nothing
            Catch ex As Exception
                obj = Nothing
                Throw New Exception("Error on  releaseObject(ByVal obj As Object) Msg:" & ex.Message.ToString)
            Finally
                GC.Collect()
            End Try
        End Sub
    End Class
End Namespace