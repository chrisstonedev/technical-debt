VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form frmServer 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Order Server"
   ClientHeight    =   4200
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   7050
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4200
   ScaleWidth      =   7050
   StartUpPosition =   3  'Windows Default
   Begin VB.TextBox txtMain 
      Height          =   2295
      Left            =   360
      MultiLine       =   -1  'True
      TabIndex        =   1
      Top             =   1200
      Width           =   6135
   End
   Begin MSWinsockLib.Winsock objWinsock 
      Left            =   120
      Top             =   360
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label lblYourIP 
      Alignment       =   2  'Center
      Height          =   255
      Left            =   480
      TabIndex        =   2
      Top             =   600
      Width           =   3255
   End
   Begin VB.Label txtStatus 
      Caption         =   "Status: Waiting for connection..."
      Height          =   375
      Left            =   600
      TabIndex        =   0
      Top             =   240
      Width           =   4575
   End
End
Attribute VB_Name = "frmServer"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()
    lblYourIP.Caption = "Listening on: " & objWinsock.LocalIP & " ; 187"

    Dim intFile As Integer
    Dim strFile As String
    strFile = "DB_FIELDS.TXT"
    intFile = FreeFile
    'Read
    Dim strLine As String
    On Error Resume Next
    Open strFile For Input As #intFile
    If Err.Number = 53 Then
        If MsgBox("Database files cannot be found. Would you like to create them now?", vbYesNo, "OrderServer") = vbYes Then
            Open strFile For Output As #intFile
            Print #intFile, _
                "CEnter customer ID   " & vbCrLf & _
                "PEnter product ID    "
            Close #intFile
        Else
            End
        End If
    End If

    objWinsock.Close
    objWinsock.LocalPort = CLng(187)
    objWinsock.Listen
End Sub

Private Sub objWinsock_Close()
    txtStatus.Caption = "Status: Disconnected from client"

    objWinsock.Close
    objWinsock.LocalPort = CLng(187)
    objWinsock.Listen
End Sub

Private Sub objWinsock_ConnectionRequest(ByVal requestID As Long)
    Dim intFile As Integer
    Dim strFile As String
    Dim strLine As String
    Dim strTemp As String

    If objWinsock.State <> sckClosed Then
        objWinsock.Close
    End If
    objWinsock.Accept requestID
    txtStatus.Caption = "Status: Connected to client"
    txtMain.SelText = "SENT: " & "C" & vbCrLf
    strFile = "DB_AUDIT.TXT"
    intFile = FreeFile
    Open strFile For Append As #intFile
    Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECould not read data from client"
    Close intFile
    objWinsock.SendData "C"
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2, strData3 As String
    Dim objDocument As DOMDocument60
    Dim blnSuccess As Boolean
    Dim objNode As IXMLDOMNode
    Dim objNodeList As IXMLDOMNodeList
    Dim strJson As String
    Dim objFieldsToSave As Collection
    Dim objResponse As clsResponse
    Dim intFile As Integer
    Dim strFile As String
    Dim strLine As String
    Dim strTemp As String
    Dim strFieldsFile As String
    Dim intFieldsFile As Integer
    Dim strFieldsLine As String

    On Error GoTo Log_Error

    Call objWinsock.GetData(strData, vbString)

    strFile = "DB_AUDIT.TXT"
    strFieldsFile = "DB_FIELDS.TXT"
    intFile = FreeFile
    Open strFile For Append As #intFile
    txtMain.SelText = "RECEIVED: " & strData & vbCrLf
    Print #intFile, "R" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & strData

    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    Select Case strData2
        Case "T"
            strData3 = Left(strData, 1)
            strData = Mid(strData, 2)
            Dim blnSuccessfulValidation As Boolean
            Select Case strData3
                Case "C"
                    If IsNumeric(strData) Then
                        blnSuccessfulValidation = True
                    Else
                        txtMain.SelText = "SENT: " & "ECustomer ID could not be found" & vbCrLf
                        Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECustomer ID could not be found"
                        objWinsock.SendData "ECustomer ID could not be found"
                    End If
                Case "P"
                    If Len(strData) > 2 Then
                        If Len(strData) <= 10 Then
                            blnSuccessfulValidation = True
                        Else
                            txtMain.SelText = "SENT: " & "EProduct ID is too long" & vbCrLf
                            Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "EProduct ID is too long"
                            objWinsock.SendData "EProduct ID is too long"
                        End If
                    Else
                        txtMain.SelText = "SENT: " & "EProduct ID is not long enough" & vbCrLf
                        Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "EProduct ID is not long enough"
                        objWinsock.SendData "EProduct ID is not long enough"
                    End If
            End Select

            Dim blnUseTheNextOne As Boolean
            Dim strLineToUse As String

            intFieldsFile = FreeFile
            If blnSuccessfulValidation Then
                strFieldsFile = "DB_FIELDS.TXT"
                Open strFieldsFile For Input As #intFieldsFile
                Do While Not EOF(intFieldsFile)
                    Line Input #intFieldsFile, strFieldsLine
                    If blnUseTheNextOne Then
                        strLineToUse = strFieldsLine
                        blnUseTheNextOne = False
                    End If
                    If Left(strFieldsLine, 1) = strData3 Then
                        blnUseTheNextOne = True
                    End If
                Loop
                Close #intFieldsFile

                Dim strFieldName As String
                Select Case Left(strLineToUse, 1)
                    Case "C"
                        strFieldName = "Customer  "
                    Case "P"
                        strFieldName = "Product   "
                    Case "Q"
                        strFieldName = "Quantity  "
                    Case "R"
                        strFieldName = "Price     "
                End Select
                strLineToUse = Left(strLineToUse, 1) & strFieldName & Mid(strLineToUse, 2)

                txtMain.SelText = "SENT: " & "R" & strLineToUse & vbCrLf
                Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "R" & strLineToUse
                objWinsock.SendData "R" & strLineToUse
            End If
        Case "S"
            intFieldsFile = FreeFile
            Open strFieldsFile For Input As #intFieldsFile
            Line Input #intFieldsFile, strFieldsLine
            Close #intFieldsFile

            Dim strFirstFieldName As String
            Select Case Left(strFieldsLine, 1)
                Case "C"
                    strFirstFieldName = "Customer  "
                Case "P"
                    strFirstFieldName = "Product   "
                Case "Q"
                    strFirstFieldName = "Quantity  "
                Case "R"
                    strFirstFieldName = "Price     "
            End Select
            strFieldsLine = Left(strFieldsLine, 1) & strFirstFieldName & Mid(strFieldsLine, 2)
            txtMain.SelText = "SENT: " & "R" & strFieldsLine & vbCrLf
            Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "R" & strFieldsLine
            objWinsock.SendData "R" & strFieldsLine
        Case "F"
            Set objDocument = New DOMDocument60
            blnSuccess = objDocument.loadXML(strData)
            If blnSuccess Then
                Set objNodeList = objDocument.selectNodes("/xml/*")
                Set objFieldsToSave = New Collection
                For Each objNode In objNodeList
                    Set objResponse = New clsResponse
                    objResponse.FieldID = objNode.Attributes.getNamedItem("id").Text
                    objResponse.UserResponse = objNode.Text
                    objFieldsToSave.Add objResponse
                Next objNode
                For Each objResponse In objFieldsToSave
                    'txtMain.SelText = "Client wants to use data """ & objResponse.UserResponse & """ for field of id """ & objResponse.FieldID & """" & vbCrLf
                Next objResponse
                'TODO: Attempt to save to database.
                txtMain.SelText = "SENT: " & "F" & vbCrLf
                Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "F"
                objWinsock.SendData "F"
            Else
                txtMain.SelText = "SENT: " & "ECould not read data from client" & vbCrLf
                Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECould not read data from client"
                objWinsock.SendData "ECould not read data from client"
            End If
    End Select

Done:

    Close #intFile

    Exit Sub

Log_Error:
    Dim ErrorNumber As Long
    Dim ErrorDescription As String
    ErrorNumber = Err.Number
    ErrorDescription = Err.Description

    On Error Resume Next
    Print #intFile, "E" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & CStr(ErrorNumber) & ": " & ErrorDescription
    If Err.Number > 0 Then
        MsgBox "Serious error alert!" & vbCrLf & CStr(ErrorNumber) & ": " & ErrorDescription, vbCritical, "Order Server"
    End If
    
    GoTo Done
End Sub

