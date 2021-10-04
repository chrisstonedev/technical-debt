VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form frmClient 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Order Client"
   ClientHeight    =   4200
   ClientLeft      =   3330
   ClientTop       =   1080
   ClientWidth     =   7050
   LinkTopic       =   "Form3"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   4200
   ScaleWidth      =   7050
   Begin VB.CommandButton cmdStart 
      Caption         =   "S&tart"
      Default         =   -1  'True
      Height          =   315
      Left            =   1560
      TabIndex        =   5
      Top             =   1320
      Width           =   975
   End
   Begin VB.TextBox txtData 
      Height          =   285
      Left            =   120
      MaxLength       =   100
      TabIndex        =   1
      Top             =   3840
      Width           =   5655
   End
   Begin VB.CommandButton cmdSend 
      Caption         =   "&Send"
      Height          =   315
      Left            =   5880
      TabIndex        =   0
      Top             =   3840
      Width           =   975
   End
   Begin MSWinsockLib.Winsock objWinsock 
      Left            =   600
      Top             =   360
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label lblQuestion 
      Caption         =   "Click to Start!"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   9.75
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   600
      TabIndex        =   6
      Top             =   2280
      Width           =   4575
   End
   Begin VB.Label txtStatus 
      Caption         =   "Status: Waiting for connection..."
      Height          =   375
      Left            =   0
      TabIndex        =   4
      Top             =   0
      Width           =   4575
   End
   Begin VB.Label lblClientNickname 
      Height          =   135
      Left            =   360
      TabIndex        =   3
      Top             =   4320
      Visible         =   0   'False
      Width           =   255
   End
   Begin VB.Label lblServerNickname 
      Height          =   135
      Left            =   120
      TabIndex        =   2
      Top             =   4320
      Visible         =   0   'False
      Width           =   255
   End
End
Attribute VB_Name = "frmClient"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Question As String

Private Sub cmdStart_Click()
    objWinsock.SendData "S"
End Sub

Private Sub objWinsock_Close()
    txtStatus.Caption = "Status: Disconnected from server"
    lblQuestion = ""
    cmdStart.Visible = True
    cmdStart.Enabled = False
    txtData.Visible = False
    cmdSend.Visible = False
End Sub

Private Sub objWinsock_DataArrival(ByVal bytesTotal As Long)
    Dim strData, strData2, strData3 As String
    Call objWinsock.GetData(strData, vbString)
    strData2 = Left(strData, 1)
    strData = Mid(strData, 2)
    Select Case strData2
        Case "C"
            lblServerNickname.Caption = strData
            Me.Show
            Unload frmStartup
            objWinsock.SendData "N" & lblClientNickname.Caption
            txtStatus.Caption = "Status: Connected to Server"
            lblQuestion = "Click to Start!"
            cmdStart.Visible = True
            cmdStart.Enabled = True
            txtData.Visible = False
            cmdSend.Visible = False
        Case "R"
            strData3 = Left(strData, 1)
            strData = Mid(strData, 2)
            lblQuestion.Caption = strData
            Question = strData3
            txtData.Visible = True
            cmdSend.Visible = True
            cmdStart.Visible = False
        Case "F"
            lblQuestion.Caption = "Order has been complete!"
            txtData.Visible = False
            cmdSend.Visible = False
            cmdStart.Visible = True
    End Select
End Sub

Private Sub cmdSend_Click()
    objWinsock.SendData "T" & Question & txtData.Text
    txtData.Text = ""
End Sub
