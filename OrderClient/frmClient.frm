VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form frmClient 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Order Client"
   ClientHeight    =   4080
   ClientLeft      =   3330
   ClientTop       =   1080
   ClientWidth     =   7050
   LinkTopic       =   "Form3"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4080
   ScaleWidth      =   7050
   Begin VB.CommandButton cmdCancel 
      Cancel          =   -1  'True
      Caption         =   "&Cancel"
      Height          =   315
      Left            =   120
      TabIndex        =   7
      Top             =   3360
      Width           =   975
   End
   Begin VB.Frame Frame1 
      Height          =   1815
      Left            =   480
      TabIndex        =   1
      Top             =   360
      Width           =   5895
      Begin VB.Label lblError 
         Alignment       =   2  'Center
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         ForeColor       =   &H000000FF&
         Height          =   375
         Left            =   120
         TabIndex        =   3
         Top             =   1320
         Width           =   5655
      End
      Begin VB.Label lblQuestion 
         Alignment       =   2  'Center
         Caption         =   "Waiting for connection..."
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   9.75
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   1095
         Left            =   60
         TabIndex        =   2
         Top             =   150
         Width           =   5775
      End
   End
   Begin VB.CommandButton cmdStart 
      Caption         =   "S&tart"
      Enabled         =   0   'False
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   13.5
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   675
      Left            =   2400
      TabIndex        =   4
      Top             =   2400
      Width           =   2175
   End
   Begin VB.TextBox txtData 
      Height          =   285
      Left            =   1320
      MaxLength       =   100
      TabIndex        =   5
      Top             =   3360
      Width           =   4335
   End
   Begin VB.CommandButton cmdSend 
      Caption         =   "&Send"
      Default         =   -1  'True
      Height          =   315
      Left            =   5880
      TabIndex        =   6
      Top             =   3360
      Width           =   975
   End
   Begin MSWinsockLib.Winsock objWinsock 
      Left            =   600
      Top             =   360
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label txtStatus 
      Caption         =   "Status: Waiting for connection..."
      Height          =   375
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   4575
   End
End
Attribute VB_Name = "frmClient"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private m_strFieldID As String
Private m_strFieldName As String
Private m_objResponses As Collection

Private Sub cmdCancel_Click()
    If MsgBox("Are you sure?", vbYesNo) = vbYes Then
        lblQuestion.Caption = "Click Start to begin!"
        lblError.Caption = ""
        cmdStart.Visible = True
        cmdCancel.Visible = False
        txtData.Visible = False
        cmdSend.Visible = False
    End If
End Sub

Private Sub cmdSend_Click()
    Dim objResponse As clsResponse
    Dim strTemp As String
    If Len(m_strFieldID) > 0 Then
        objWinsock.SendData "T" & m_strFieldID & txtData.Text
        cmdCancel.Enabled = False
        txtData.Enabled = False
        cmdSend.Enabled = False
    Else
        strTemp = "<xml>"
        For Each objResponse In m_objResponses
            strTemp = strTemp & "<response id=""" & objResponse.FieldID & """>" & objResponse.UserResponse & "</response>"
        Next objResponse
        strTemp = strTemp & "</xml>"
        objWinsock.SendData "F" & strTemp
        cmdCancel.Enabled = False
        txtData.Enabled = False
        cmdSend.Enabled = False
    End If
End Sub

Private Sub cmdStart_Click()
    objWinsock.SendData "S"
    m_strFieldID = ""
    Set m_objResponses = New Collection
End Sub

Private Sub Form_Load()
    objWinsock.Connect objWinsock.LocalIP, "187"
End Sub

Private Sub objWinsock_Close()
    txtStatus.Caption = "Status: Disconnected from server"
    lblQuestion = ""
    cmdStart.Visible = True
    cmdStart.Enabled = False
    cmdCancel.Visible = False
    txtData.Visible = False
    cmdSend.Visible = False
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2, strData3 As String
    Dim objResponse As clsResponse
    Call objWinsock.GetData(strData, vbString)
    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    Select Case strData2
        Case "C" 'Connection request
            txtStatus.Caption = "Status: Connected to Server"
            lblQuestion = "Click to Start!"
            cmdStart.Visible = True
            cmdStart.Enabled = True
            cmdCancel.Visible = False
            txtData.Visible = False
            cmdSend.Visible = False
        Case "R" 'Request for more data
            strData3 = Left(strData, 1)
            strData = Mid(strData, 2)
            If Len(m_strFieldID) > 0 Then
                Set objResponse = New clsResponse
                objResponse.FieldID = m_strFieldID
                objResponse.FieldName = m_strFieldName
                objResponse.UserResponse = txtData.Text
                m_objResponses.Add objResponse
            End If
            m_strFieldID = strData3
            If Len(m_strFieldID) > 0 Then
                m_strFieldName = Trim(Left(strData, 10))
                strData = Trim(Mid(strData, 11))
                lblQuestion.Caption = strData
                lblError.Caption = ""
                cmdStart.Visible = False
                cmdCancel.Visible = True
                cmdCancel.Enabled = True
                txtData.Visible = True
                txtData.Enabled = True
                txtData.Text = ""
                txtData.SetFocus
                cmdSend.Visible = True
                cmdSend.Enabled = True
                cmdSend.Caption = "&Send"
            Else
                lblQuestion.Caption = "Please confirm all fields, then submit:"
                For Each objResponse In m_objResponses
                    lblQuestion.Caption = lblQuestion.Caption & vbCrLf & objResponse.FieldName & ": " & objResponse.UserResponse
                Next objResponse
                lblError.Caption = ""
                cmdStart.Visible = False
                cmdCancel.Visible = True
                cmdCancel.Enabled = True
                txtData.Visible = False
                txtData.Enabled = True
                txtData.Text = ""
                cmdSend.Visible = True
                cmdSend.Enabled = True
                cmdSend.Caption = "&Submit"
                cmdSend.SetFocus
            End If
        Case "F" 'Finished order
            lblQuestion.Caption = "Order has been complete!"
            lblError.Caption = ""
            cmdStart.Visible = True
            cmdCancel.Visible = False
            txtData.Visible = False
            cmdSend.Visible = False
        Case "E" 'Error on input
            lblError.Caption = strData
            cmdCancel.Enabled = True
            cmdSend.Enabled = True
            If txtData.Visible Then
                txtData.Enabled = True
                txtData.SelStart = 0
                txtData.SelLength = Len(txtData.Text)
                txtData.SetFocus
            Else
                cmdSend.SetFocus
            End If
    End Select
End Sub
