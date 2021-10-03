VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "MSWINSCK.OCX"
Begin VB.Form frmStartup 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "Connect as Server"
   ClientHeight    =   2340
   ClientLeft      =   2520
   ClientTop       =   3405
   ClientWidth     =   2400
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   2340
   ScaleWidth      =   2400
   Begin VB.TextBox txtNickname 
      Height          =   285
      Left            =   120
      MaxLength       =   20
      TabIndex        =   2
      Text            =   "Enter nickname"
      Top             =   1200
      Width           =   2175
   End
   Begin MSWinsockLib.Winsock objWinsock 
      Left            =   1920
      Top             =   1680
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.CommandButton cmdConnect 
      Caption         =   "&Connect"
      Default         =   -1  'True
      Height          =   375
      Left            =   720
      TabIndex        =   0
      Top             =   1920
      Width           =   975
   End
   Begin VB.Label lblYourIP 
      Alignment       =   2  'Center
      Height          =   255
      Left            =   120
      TabIndex        =   1
      Top             =   1560
      Width           =   2175
   End
End
Attribute VB_Name = "frmStartup"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()
    lblYourIP.Caption = "Your IP:  " & objWinsock.LocalIP
End Sub

Private Sub cmdConnect_Click()
    If txtNickname.Text = "" Then
        Exit Sub
    End If
    frmServer.objWinsock.Close
    frmServer.objWinsock.LocalPort = CLng(187)
    frmServer.objWinsock.Listen
    frmServer.lblServerNickname.Caption = txtNickname.Text
End Sub

