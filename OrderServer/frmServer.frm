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

Private Const AUDIT_FILE_NAME As String = "DB_AUDIT.TXT"
Private Const FIELDS_FILE_NAME As String = "DB_FIELDS.TXT"

Private Sub Form_Load()
    lblYourIP.Caption = "Listening on: " & objWinsock.LocalIP & " ; 187"

    Dim intFile As Integer
    intFile = FreeFile
    Dim strLine As String
    On Error Resume Next
    Open FIELDS_FILE_NAME For Input As #intFile
    If Err.Number = 53 Then
        If MsgBox("Database files cannot be found. Would you like to create them now?", vbYesNo, "OrderServer") = vbYes Then
            Open FIELDS_FILE_NAME For Output As #intFile
            Print #intFile, _
                "CEnter customer ID   " & vbCrLf & _
                "PEnter product ID    "
            Close #intFile
        Else
            End
        End If
    Else
        Close #intFile
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
    intFile = FreeFile
    Open AUDIT_FILE_NAME For Append As #intFile
    Print #intFile, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECould not read data from client"
    Close intFile
    objWinsock.SendData "C"
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strRequestType As String

    On Error GoTo Log_Error

    Call objWinsock.GetData(strData, vbString)

    Dim intAuditFileReference As Integer
    intAuditFileReference = FreeFile
    Open AUDIT_FILE_NAME For Append As #intAuditFileReference
    txtMain.SelText = "RECEIVED: " & strData & vbCrLf
    Print #intAuditFileReference, "R" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & strData

    strRequestType = Left(strData, 1)
    strData = Mid(strData, 2)
    Select Case strRequestType
        Case "T"
            Dim strFieldId As String
            strFieldId = Left(strData, 1)
            Dim strUserResponse As String
            strUserResponse = Mid(strData, 2)
            ValidateUserResponse strFieldId, strUserResponse, intAuditFileReference
        Case "S"
            HandleStartRequest intAuditFileReference
        Case "F"
            CompleteOrder strData, intAuditFileReference
    End Select

Done:
    Close #intAuditFileReference
    Exit Sub

Log_Error:
    Dim ErrorNumber As Long
    ErrorNumber = Err.Number
    Dim ErrorDescription As String
    ErrorDescription = Err.Description

    On Error Resume Next
    Print #intAuditFileReference, "E" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & CStr(ErrorNumber) & ": " & ErrorDescription
    If Err.Number > 0 Then
        MsgBox "Serious error alert!" & vbCrLf & CStr(Err.Number) & ": " & Err.Description, vbCritical, "Order Server"
    Else
        MsgBox "Some error occurred..." & vbCrLf & CStr(ErrorNumber) & ": " & ErrorDescription, vbExclamation, "Order Server"
    End If
    
    GoTo Done
End Sub

Private Sub ValidateUserResponse(ByVal strFieldId As String, ByVal strData As String, ByVal intAuditFileReference As Integer)
    Dim blnTmp As Boolean
    Select Case strFieldId
        Case "C"
            If IsNumeric(strData) Then
                blnTmp = True
            Else
                txtMain.SelText = "SENT: " & "ECustomer ID could not be found" & vbCrLf
                Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECustomer ID could not be found"
                objWinsock.SendData "ECustomer ID could not be found"
            End If
        Case "P"
            If Len(strData) > 2 Then
                If Len(strData) <= 10 Then
                    blnTmp = True
                Else
                    txtMain.SelText = "SENT: " & "EProduct ID is too long" & vbCrLf
                    Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "EProduct ID is too long"
                    objWinsock.SendData "EProduct ID is too long"
                End If
            Else
                txtMain.SelText = "SENT: " & "EProduct ID is not long enough" & vbCrLf
                Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "EProduct ID is not long enough"
                objWinsock.SendData "EProduct ID is not long enough"
            End If
    End Select

    Dim blnNxt As Boolean
    Dim strLn As String

    Dim intFieldsFileReference As Integer
    intFieldsFileReference = FreeFile
    If Not blnTmp Then
        Exit Sub
    End If

    Dim strFieldsFileLine As String
    Open FIELDS_FILE_NAME For Input As #intFieldsFileReference
    Do While Not EOF(intFieldsFileReference)
        Line Input #intFieldsFileReference, strFieldsFileLine
        If blnNxt Then
            strLn = strFieldsFileLine
            blnNxt = False
        End If
        If Left(strFieldsFileLine, 1) = strFieldId Then
            blnNxt = True
        End If
    Loop
    Close #intFieldsFileReference

    Dim strFN As String
    Select Case Left(strLn, 1)
        Case "C"
            strFN = "Customer  "
        Case "P"
            strFN = "Product   "
        Case "Q"
            strFN = "Quantity  "
        Case "R"
            strFN = "Price     "
    End Select
    strLn = Left(strLn, 1) & strFN & Mid(strLn, 2)

    txtMain.SelText = "SENT: " & "R" & strLn & vbCrLf
    Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "R" & strLn
    objWinsock.SendData "R" & strLn
End Sub

Private Sub HandleStartRequest(ByVal intAuditFileReference As Integer)
    Dim intFieldsFileReference As Integer
    intFieldsFileReference = FreeFile
    Open FIELDS_FILE_NAME For Input As #intFieldsFileReference
    Dim strFieldsFileLine As String
    Line Input #intFieldsFileReference, strFieldsFileLine
    Close #intFieldsFileReference

    Dim strFieldName As String
    Select Case Left(strFieldsFileLine, 1)
        Case "C"
            strFieldName = "Customer  "
        Case "P"
            strFieldName = "Product   "
        Case "Q"
            strFieldName = "Quantity  "
        Case "R"
            strFieldName = "Price     "
    End Select
    strFieldsFileLine = Left(strFieldsFileLine, 1) & strFieldName & Mid(strFieldsFileLine, 2)
    txtMain.SelText = "SENT: " & "R" & strFieldsFileLine & vbCrLf
    Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "R" & strFieldsFileLine
    objWinsock.SendData "R" & strFieldsFileLine
End Sub

Private Sub CompleteOrder(ByVal strData As String, ByVal intAuditFileReference As Integer)
    Dim objNewMethods As OrderCore_ServerFunctions.NewMethods
    Set objNewMethods = New OrderCore_ServerFunctions.NewMethods

    Dim strDatabaseFormat As String
    strDatabaseFormat = objNewMethods.ProcessCompleteOrderRequest(strData)
    
    If Len(strDatabaseFormat) = 0 Then
        txtMain.SelText = "SENT: " & "ECould not read data from client" & vbCrLf
        Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "ECould not read data from client"
        objWinsock.SendData "ECould not read data from client"
        Exit Sub
    Else
        Dim intOrdersFile As Integer
        Dim strOrdersFile As String
    
        strOrdersFile = "DB_ORDERS.TXT"
        intOrdersFile = FreeFile
        Open strOrdersFile For Append As #intOrdersFile
        Print #intOrdersFile, strDatabaseFormat
        Close intOrdersFile
    
        txtMain.SelText = "SENT: " & "F" & vbCrLf
        Print #intAuditFileReference, "S" & Format(Now(), "yyyy-mm-dd hh:nn:ss") & "F"
        objWinsock.SendData "F"
    End If
End Sub
